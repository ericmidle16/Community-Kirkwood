using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DataDomain;
using LogicLayer;
using EventManager = LogicLayer.EventManager;
using Task = DataDomain.Task;

namespace WpfPresentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewCalendarDesktop.xaml
    /// </summary>
    public partial class ViewCalendarDesktop : Page
    {
        MainWindow main = MainWindow.GetMainWindow(); // Main window Reference

        EventManager _eventManager = new EventManager();
        ProjectManager _projectManager = new ProjectManager();
        TaskManager _taskManager = new TaskManager();

        private DateTime _currentDate = DateTime.Today;
        private DateTime? _selectedDate = null;

        //private string mode;

        private List<Task> _taskList = new();
        private List<Event> _events = new();
        private List<ProjectVM> _projects = new();



        public ViewCalendarDesktop()
        {
            InitializeComponent();
            SeedData();
            GenerateCalendar();
            
        }

        private void SeedData()
        {
            _taskList = _taskManager.GetTasksByUserID(main.UserID);
            _events = _eventManager.ViewEventListByApprovedUserID(main.UserID);
            _projects = _projectManager.GetAllApprovedProjectsByUserID(main.UserID);
        }

        private void GenerateCalendar()
        {
            MonthYearLabel.Content = _currentDate.ToString("MMMM yyyy");
            DaysGridControl.Children.Clear();

            var first = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int days = DateTime.DaysInMonth(first.Year, first.Month);
            int padding = (int)first.DayOfWeek;

            for (int i = 0; i < padding; i++)
                DaysGridControl.Children.Add(new Border());

            for (int day = 1; day <= days; day++)
            {
                DateTime date = new DateTime(_currentDate.Year, _currentDate.Month, day);

                var dotPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(2, 2, 2, 2) };

                if (_projects.Any(p => date.Date == p.StartDate.Date))
                    dotPanel.Children.Add(CreateDot(Brushes.Blue));

                if (_taskList.Any(t => t.ProjectID != 0 && t.TaskDate.Date == date.Date && t.Active))
                    dotPanel.Children.Add(CreateDot(Brushes.Green));

                if (_events.Any(e => e.StartDate.Date <= date.Date && e.EndDate.Date >= date.Date && e.Active))
                    dotPanel.Children.Add(CreateDot(Brushes.Orange));

                if (_taskList.Any(t => t.EventID != 0 && t.TaskDate.Date == date.Date && t.Active))
                    dotPanel.Children.Add(CreateDot(Brushes.Yellow));

                var dayBox = new StackPanel
                {
                    Tag = date,
                    Cursor = Cursors.Hand,
                    Margin = new Thickness(1),
                    Background = Brushes.WhiteSmoke,
                    MinHeight = 75
                };
                dayBox.MouseLeftButtonDown += Day_Clicked;

                dayBox.Children.Add(new TextBlock
                {
                    Text = date.Day.ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5, 2, 5, 0)
                });

                dayBox.Children.Add(dotPanel);
                DaysGridControl.Children.Add(dayBox);
            }
        }

        private Ellipse CreateDot(Brush color)
        {
            return new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = color,
                Margin = new Thickness(2, 0, 2, 0)
            };
        }

        private void Day_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is StackPanel stack && stack.Tag is DateTime date))
                return;

            _selectedDate = date;

            
            List<ScheduledObject> displayItems = new List<ScheduledObject>();

                
            foreach(ProjectVM p in _projects)
            {
                if(p.StartDate.Date == date.Date)
                {
                    displayItems.Add(new ScheduledObject
                    {
                        Title = p.Name + " " + p.LocationName,
                        Time = "(All Day)",
                        Type = "Project",
                        ThingScheduled = p
                    });
                }
            }
            displayItems.AddRange(_projects
                .Where(p => p.StartDate.Date == date.Date)
                .Select(p => new ScheduledObject
                {

                }));

            displayItems.AddRange(_taskList
                .Where(t => t.ProjectID != 0
                         && t.TaskDate.Date == date.Date
                         && t.Active)
                .Select(t => new ScheduledObject
                {
                    Title = t.Name,
                    Time = t.TaskDate.ToString("hh:mm tt"),
                    Type = "Project Task",
                    ThingScheduled = t
                }));

            displayItems.AddRange(_events
                .Where(ev => ev.Active
                          && date >= ev.StartDate
                          && date <= ev.EndDate)
                .Select(ev => new ScheduledObject
                {
                    Title = ev.Name,
                    Time = "(All Day)",
                    Type = "Event",
                    ThingScheduled = ev
                }));

            displayItems.AddRange(_taskList
                .Where(t => t.EventID != 0
                         && t.TaskDate.Date == date.Date
                         && t.Active)
                .Select(t => new ScheduledObject
                {
                    Title = t.Name,
                    Time = t.TaskDate.ToString("hh:mm tt"),
                    Type = "Event Task",
                    ThingScheduled = t
                }));

            EventDataGrid.ItemsSource = displayItems;
        }

        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            GenerateCalendar();
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            GenerateCalendar();
        }

        private void ViewFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (_selectedDate.HasValue)
            //{
            //    Day_Clicked(new StackPanel { Tag = _selectedDate.Value }, null);
            //}
            
            //GenerateCalendar();
        }

        private void btnViewScheduledObject_Click(object sender, RoutedEventArgs e)
        {
            if(EventDataGrid.SelectedItem is ScheduledObject sel)
            {
                var obj = sel.ThingScheduled;
                if(obj is ProjectVM proj)
                {
                    if (main.HasProjectRole("Volunteer", proj.ProjectID))
                    {
                        NavigationService.GetNavigationService(this)?.Navigate(new viewProjectDesktop(proj.ProjectID));
                    }
                    else
                    {
                        return;
                    }
                }
                else if(obj is Event evt)
                {
                    IEventManager eventManager = new EventManager();
                    Event result = eventManager.SelectEventByID(evt.EventID);
                    if (main.HasProjectRole("Volunteer", evt.ProjectID))
                    {
                        NavigationService.GetNavigationService(this)?.Navigate(new PgViewAnEvent(evt, eventManager));
                    }
                    else
                    {
                        return;
                    }
                    
                }
                else if(obj is Task task)
                {
                    if (main.HasProjectRole("Volunteer", task.ProjectID))
                    {
                        NavigationService.GetNavigationService(this)?.Navigate(new viewSingleTask(task.TaskID));
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }  
}
public class ScheduledObject
{
    public string Title { get; set; }
    public string Time { get; set; }
    public string Type { get; set; }
    public Object ThingScheduled { get; set; } // Holds the project, event, or task
}
