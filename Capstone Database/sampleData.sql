print '' print '*** Sample Data for the Entire Project ***'
GO

print '' print '*** using database communityDb'
GO
USE [communityDb]
GO

/*
	<summary>
	Creator: Skyann Heintz, Jennifer Nicewanner, Kate Rich
	Created: 2025-05-01
	Summary: This inserts data into the SystemRole table.
			It inserts the user role and the description
			of that role.
	</summary>
*/
print '' print '*** Insert SystemRole Test Records ***'
GO
INSERT INTO [dbo].[SystemRole]
        ([SystemRoleID], [Description])
    VALUES
		('Admin', 'System Administrator'),
        ('User', 'Commmunity Member')
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Josh Nicholson
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Skill table.
	</summary>
*/
print '' print '*** Insert Skill Test Records ***'
GO
INSERT INTO [dbo].[Skill] 
	([SkillID], [Description])
VALUES 
	('Cooking', 'The ability to prepare meals safely and efficiently, considering nutrition, hygiene, and dietary preferences.'),
	('Cleaning', 'Maintaining hygiene and organization in spaces by removing dirt, clutter, and germs to create a safe and welcoming environment.'),
	('Driving', 'Operating a vehicle safely and responsibly to transport people or goods while following traffic laws and safety regulations.'),
	('Building', 'Constructing, repairing, or assembling structures using tools, materials, and techniques suited to the project’s needs.'),
	('Leadership', 'Guiding and motivating a team, making decisions, and managing resources effectively to achieve shared goals.'),
	('Concrete work', 'Framing, pouring, and finishing concrete work.'),
	('Backhoe', 'Operating a backhoe.'),
	('Manual Labor', 'Performing physical work such as shoveling/digging, demolition, lifting up to 50 lbs.'),
	('Heavy Lifting', 'Lifting 50-100 lbs repeatedly.'),
	('Power Tools', 'Operating power tools such as circular or compound saw, nail gun, reciprocating saw, etc.'),
	('Light Labor', 'Performing light tasks such as sweeping, raking, or washing.'),
	('Mowing', 'Operating a push mower or power trimmer.'),
	('Heavy Mowing', 'Operating a riding or tractor mower, brush hog.'),
	('Lawn care', 'Operating a leaf blower, pulling weeds.'),
	('Landscaping', 'Able to help choose, plant, water, and maintain landscape plants.'),
	('Hardscaping', 'Able to help design, create, and maintain hardscape features like retaining walls, pavers, or patios.'),
	('Electrical', 'Performing installation, maintenance, or repair of electrical items.'),
	('Framing', 'Designing and completing framework.'),
	('Plumbing', 'Designing, installing, and repairing plumbing items.'),
	('Roofing', 'Tear off, install, repair of roofs.'),
	('Finish work', 'Completing detailed finish work for woodworking, trim, flooring, etc.'),
	('Drywalling', 'Able to hang, assist with hanging, or finish drywall including drywall repair.'),
	('Painting', 'Completing painting or staining of walls, furniture, etc.'),
	('Woodworking', 'Able to perform woodworking with hand or power tools.'),
	('Sewing', 'Able to operate a sewing machine or hand sew to create or repair items.'),
	('Crafting', 'Able to make decorative or creative items by hand.'),
	('Construction Management', 'Managing a construction project from start to finish.')
GO

/*
    <summary>
    Creator:            Yousif Omer, Skyann Heintz, Kate Rich, Jennifer Nicewanner
    Created:            2025-05-01
    Summary:            This is the INSERT data into the Project Type Table
    </summary>
*/
print '' print '*** INSERT data for Project Type Table'
GO
INSERT INTO [dbo].[ProjectType]
	([ProjectTypeID], [Description])
VALUES
	('Cleanup', 'With a group of volunteers, doing a cleanup of the location together.'),
    ('Planting', 'We will be planting a lot of trees and native plants to improve green spaces.'),
    ('Improvements', 'There are many places in the park that need attention, repairs, and new amenities.'),
    ('Propane Emergency', 'Providing propane and heating assistance to families in need during emergencies.'),
    ('Food Drive', 'Collecting and distributing food to support local shelters and food pantries.'),
    ('Community Garden', 'Creating or maintaining a garden where community members can grow food together.'),
    ('Neighborhood Watch', 'Organizing safety initiatives to prevent crime and improve security in neighborhoods.'),
    ('Trail Restoration', 'Repairing and maintaining hiking or biking trails in public spaces.'),
    ('Disaster Relief', 'Providing aid and support to those affected by natural disasters or emergencies.'),
    ('Graffiti Removal', 'Cleaning up vandalized areas and restoring walls with fresh paint or murals.'),
    ('Recycling Initiative', 'Encouraging waste reduction through organized recycling efforts.'),
    ('Home Repair Assistance', 'Helping seniors or low-income families with basic home repairs and maintenance.'),
    ('Community Event Support', 'Assisting with setting up and managing public events for the local community.'),
    ('Public Art Installation', 'Creating murals, sculptures, or other artwork to beautify public spaces.'),
    ('Accessibility Upgrades', 'Installing ramps, handrails, or other accessibility improvements in public areas.'),
    ('School Supply Drive', 'Gathering and distributing school supplies to children in need.'),
    ('Shelter Construction', 'Building temporary shelters for homeless individuals or disaster victims.'),
    ('Youth Mentorship Program', 'Providing mentorship and educational support to local youth.'),
    ('Senior Assistance', 'Helping elderly community members with daily tasks like transportation or companionship.');
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Ellie Wacker
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the ProjectRole table.
	</summary>
*/
print '' print '*** Insert ProjectRole Test Records ***'
GO
INSERT INTO [dbo].[ProjectRole]
	([ProjectRoleID], [Description])
	VALUES
        ('Volunteer Director', 'Oversees all volunteer operations for projects.'),
        ('Project Starter', 'Initiates new projects, defining goals, scope, and key participants.'),
        ('Volunteer', 'Assists in project activities by providing hands-on support wherever needed.'),
        ('Background Checker', 'Verifies credentials and eligibility of volunteers and project participants.'),
        ('Moderator', 'Monitors discussions, ensuring respectful and productive communication within teams.'),
        ('Accountant', 'Manages financial records, budgeting, and expense tracking for the project.'),
        ('Purchaser', 'Handles procurement of materials, supplies, and resources required for project completion.'),
        ('Scheduler', 'Coordinates team schedules and ensures timely execution of project milestones.'),
        ('Driver', 'Responsible for transportation needs, including material deliveries and personnel travel.'),
		('Event Coordinator', 'Responsible for planning events for the project and coordinating volunteers to help with them.')
GO

/*
    <summary>
    Creator:            Jennifer Nicewanner, Kate Rich, Yousif Omer
    Created:            2025-05-01
    Summary:            This is the INSERT data for Event Type table
    </summary>
*/
print '' print '*** Insert EventType Test Records ***'
GO
INSERT INTO [dbo].[EventType]
	([EventTypeID],[Description])
VALUES
	('Conference', 'A formal gathering for discussions, presentations, and networking.'),
    ('Workshop', 'A hands-on event focused on learning and skill development.'),
    ('Seminar', 'A lecture-style event aimed at sharing information and insights.'),
    ('Webinar', 'An online seminar conducted via video conferencing.'),
    ('Fundraiser', 'An event organized to raise money for a cause or project.'),
    ('Team Meeting', 'A structured discussion among project members.'),
    ('Networking Event', 'A casual gathering for professionals to connect.'),
    ('Open House', 'A special event promoting a new project.'),
    ('Award Ceremony', 'A formal event recognizing achievements and contributions.'),
    ('Training Session', 'An educational event focused on professional development.'),
    ('Social Gathering', 'An informal event for entertainment and relaxation.'),
    ('Charity Event', 'A community-driven initiative focused on supporting a cause.'),
    ('Panel Discussion', 'A moderated conversation featuring experts on a topic.'),
    ('Recruitment Fair', 'An event focused on hiring and career opportunities.'),
    ('Strategy Meeting', 'A business-focused session to define goals and plans.'),
    ('Exhibition', 'A showcase of products, services, or creative works.'),
    ('Town Hall', 'A community meeting addressing concerns and discussions.'),
    ('Q and A Session', 'An interactive event where attendees ask questions to experts.'),
    ('Picnic in the Park', 'A picnic in the park!'),
    ('Graduation Party', 'An event to celebrate graduation!'),
    ('Yard Sale', 'I know it doesn''t have to do with cleaning, but whatever.')
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Kate Rich, Ellie Wacker
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the DocumentType table.
	</summary>
*/
print '' print '*** Insert DocumentType Test Records ***'
GO
INSERT INTO [dbo].[DocumentType]
	([DocumentTypeID], [Description])
	VALUES
		('Vehicle', 'Drivers License Info or Insurance.'),
		('User', 'Background Check documentation or other documents related to a User.'),
		('Project', 'Project-related documents - permits, inspections, etc.')
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Kate Rich
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the TaskType table.
	</summary>
*/
print '' print '*** Insert TaskType Records ***'
GO
INSERT INTO [dbo].[TaskType]
	([TaskType], [Description])
VALUES
    ('Heavy Lifting', 'Transporting and moving large or heavy items.'),
    ('Construction', 'Building or assembling structures, including carpentry and masonry.'),
    ('Landscaping', 'Clearing, planting, and maintaining outdoor spaces.'),
    ('Painting', 'Applying paint or coatings to surfaces, including walls and signs.'),
    ('Assembly', 'Putting together materials or equipment for project purposes.'),
    ('Demolition', 'Breaking down old structures and removing debris.'),
    ('Digging', 'Excavating ground for planting, foundations, or infrastructure.'),
    ('Loading and Unloading', 'Handling materials for transportation or distribution.'),
    ('Shelving and Stocking', 'Organizing and replenishing supplies or inventory.'),
    ('Cleaning', 'Maintaining hygiene and tidiness in workspaces or public areas.'),
    ('Recycling and Waste Management', 'Sorting and handling waste responsibly.'),
    ('Equipment Operation', 'Using tools and machinery such as forklifts, drills, and saws.'),
    ('Fence and Barrier Setup', 'Installing protective barriers for safety and organization.'),
    ('Road and Path Maintenance', 'Repairing and maintaining walkways, roads, and trails.'),
    ('Storage Organization', 'Sorting and arranging supplies for efficient access.'),
    ('Furniture Moving', 'Transporting and placing tables, chairs, and other items.'),
    ('Warehouse Work', 'Handling logistics, packing, and material distribution.'),
    ('Food Prep and Distribution', 'Cooking, serving, and organizing food for events or relief efforts.'),
    ('Electrical Work', 'Installing or maintaining lighting and electrical systems.'),
    ('Roofing and Repairs', 'Handling roof installations or repairs as needed.'),
    ('Setup', 'Arranging venue, equipment, and materials before the event begins.'),
    ('Registration', 'Managing attendee check-in and sign-up processes.'),
    ('Fundraising', 'Organizing efforts to raise money for the community project.'),
    ('Volunteer Coordination', 'Assigning and managing volunteer roles and responsibilities.'),
    ('Social Media Promotion', 'Creating and sharing content to spread awareness about the event.'),
    ('Public Relations', 'Communicating with media and stakeholders to promote the event.'),
    ('Logistics', 'Handling transportation, supplies, and scheduling needs.'),
    ('Guest Management', 'Ensuring VIPs, speakers, or special guests are accommodated.'),
    ('Tech Support', 'Providing assistance with audio-visual setup, streaming, or IT troubleshooting.'),
    ('Security', 'Maintaining a safe environment and managing crowd control.'),
    ('Food Service', 'Coordinating catering or refreshments for participants.'),
    ('Activity Facilitation', 'Leading workshops, games, or interactive segments.'),
    ('Cleanup', 'Restoring the venue to its original condition after the event.'),
    ('Documentation', 'Taking photos, recording sessions, and compiling event reports.'),
    ('Survey Collection', 'Gathering attendee feedback through forms or interviews.'),
    ('Networking Coordination', 'Encouraging interaction and engagement among attendees.'),
    ('Medical Support', 'Providing first aid and addressing health-related concerns.'),
    ('Accessibility Assistance', 'Ensuring accommodations for attendees with special needs.'),
    ('Follow-up Communication', 'Sending thank-you notes and updates after the event.'),
    ('Community Outreach', 'Engaging with local organizations to extend project impact.')
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Kate Rich
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the TaskType table.
	</summary>
*/
print '' print '*** Insert ExpenseType Test Records ***'
GO
INSERT INTO [dbo].[ExpenseType]
	([ExpenseTypeID], [Description])
VALUES
	('Rent/Utilities','Costs associated with business physical location.'),
	('Insurance', 'Costs of protecting business against various risks.'),
	('Marketing/Advertising', 'Costs related to prompting volunteers'),
	('Bank Fees', 'Fees charged by banking services.'),
	('Travel/Entertainment', 'Costs including traveling and entertainment.'),
	('Accounting/Legal Fees', 'Costs for professional services.'),
	('Other', 'An expense type not listed.')
GO

/*
	<summary>
		Creator:	Kate Rich, Christivie Mauwa
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the DonationType table.
	</summary>
*/
print '' print '*** Insert DonationType Test Records ***'
GO
INSERT INTO [dbo].[DonationType]
	([DonationType], [Description])
	VALUES
		('Monetary', 'Monetary/Cash Donation'),
		('Goods', 'Donation of goods'),
		('Services', 'Donation of services')
GO

/* 
	<summary>
	Creator:            Jacob McPherson, Kate Rich, Jennifer Nicewanner
	Created:            2025-05-01
	Summary:            Population for ExternalContactType table  
	</summary> 
*/
PRINT '' PRINT '*** Insert ExternalContactType Test Records ***'
GO
INSERT INTO [dbo].[ExternalContactType]
	([ExternalContactTypeID], [Description])
VALUES
	('Plumber', 'Plumbing Repair'),
	('Electrician', 'Repair of Electrical Systems'),
	('Builder', 'Building Physical Structures'),
	('Carpenter', 'Woodworking, furniture creation, and structural assembly'),
	('HVAC Technician', 'Heating, ventilation, and air conditioning maintenance'),
	('Painter', 'Interior and exterior painting for buildings and structures'),
	('Landscaper', 'Outdoor design, gardening, and maintenance'),
	('Mechanic', 'Automobile and machinery repair services'),
	('Roofing Specialist', 'Installation and repair of roofing structures'),
	('Welder', 'Metal fabrication and welding services'),
	('Mason', 'Brick, stone, and concrete work for construction'),
	('Locksmith', 'Security systems, locks, and access control services'),
	('Pest Control', 'Extermination and prevention of pest infestations'),
	('IT Technician', 'Computer and network support services'),
	('Glass Installer', 'Window and glass installation and repair'),
	('Surveyor', 'Land measurement and mapping services'),
	('Environmental Consultant', 'Sustainability and environmental impact assessment'),
	('Disaster Recovery Specialist', 'Cleanup and restoration after emergencies'),
	('Security Officer', 'Monitoring and safeguarding property and personnel'),
	('Waste Management', 'Collection and disposal of waste and recyclable materials')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner, Skyann Heintz, Brodie Pasker, Jacob McPherson, Yousif Omer
		Created:	2025-02-03
		Summary:	Script for inserting sample data into the User table.
	</summary>
*/
print '' print '*** Insert User Test Records ***'
GO
INSERT INTO [dbo].[User]
	([GivenName], [FamilyName], [PhoneNumber], [Email], [City], [State], [Image], [ImageMimeType], [ReactivationDate], [Suspended], [ReadOnly], [Active], [RestrictionDetails], [Biography])        
	VALUES
		('Hank', 'Hill', '555-867-5309', 'hhill@stricklandpropane.com', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A hardworking propane salesman in Arlen, Texas.  A proud family man who values tradition, responsibility, and a well-maintained lawn.'),
        ('Peggy', 'Hill', '555-123-4567', 'phill@tomlandryms.edu', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A confident and sometimes overly self-assured substitute teacher, Peggy believes she''s an expert in everything from Spanish to Boggle.'),
        ('John', 'Redcorn', '555-999-5819', 'johnredcorn@stricklandpropane.com', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A Native American healer, musician and activist.'),
        ('Luanne', 'Platter', '748-123-9999', 'lplatter@tomlandryms.edu', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A sweet but naive young woman who grows into a responsible mother and hairstylist.'),
        ('Dale', 'Gribble', '585-887-8888', 'dgribble@stricklandpropane.com', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A conspiracy theorist and exterminator. Always ready with a wild theory.'),
        ('Bill', 'Dauterive', '555-123-4567', 'bdauterive@tomlandryms.edu', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A lonely, kind-hearted Army barber who struggles with self-esteem and often finds himself in unfortunate situations.'),
        ('Joseph', 'Gribble', '755-877-7777', 'jgribble@stricklandpropane.com', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A fun-loving, slightly dim-witted teenager who enjoys sports and conspiracy theories.'),
        ('Nancy', 'Gribble', '665-623-6666', 'ngribble@tomlandryms.edu', 'Arlen', 'TX', null, null, null, 0, 0, 1, '', 'A charismatic and ambitious weather reporter.'),
		('Frank', 'Chips', '15632297634', 'Frank@Company.com', 'Cedar Rapids', 'IA', null, null, null, 0, 0, 1, '', 'Fearless Leader'),
		('Jane', 'Doe', '13195550001', 'Jane@Company.com', 'Ely', 'IA', null, null, '2026-06-01', 1, 0, 1, 'Suspended for stealing', 'Union Plumber'),
		('John', 'Doe', '1234567890123', 'john.doe@example.com', 'New York', 'NY', NULL, NULL, NULL, 0, 1, 1, '', 'An anonymous man'),
		('Jane', 'Smith', '9876543210987', 'jane.smith@example.com', 'Los Angeles', 'CA', NULL, NULL, NULL, 1, 0, 1, 'Suspended for being rude to the elderly', 'I love animals and helping the elderly.'),
		('Alice', 'Johnson', '5551234567890', 'alice.johnson@example.com', 'Chicago', 'IL', NULL, NULL, NULL, 1, 0, 1, 'Account suspended due to project policy violation', 'I love following the rules and just want to help my community!'),
		('Bob', 'Williams', '4449876543210', 'bob.williams@example.com', 'Houston', 'TX', NULL, NULL, '2024-12-01', 0, 1, 1, 'Read-only access assigned for compliance', ''),
		('Charlie', 'Brown', '3338765432109', 'charlie.brown@example.com', 'Miami', 'FL', NULL, NULL, NULL, 1, 0, 1, 'Did not want to work with the blockheads', 'Good grief!'),
		('Diana', 'Prince', '2227654321098', 'diana.prince@example.com', 'Seattle', 'WA', NULL, NULL, '2025-01-15', 0, 0, 1, '', ''),
		('Ethan', 'Hunt', '1116543210987', 'ethan.hunt@example.com', 'Denver', 'CO', NULL, NULL, NULL, 1, 0, 1, 'Under investigation for multiple login attempts to others'' accounts', 'I''m an IT specialist looking to help with project ;)'),
		('Fiona', 'Gallagher', '9995432109876', 'fiona.gallagher@example.com', 'Boston', 'MS', NULL, NULL, NULL, 0, 0, 1, '', 'Don''t ask me about my husband..'),
		('George', 'Costanza', '8884321098765', 'george.costanza@example.com', 'San Francisco', 'CA', NULL, NULL, '2024-11-20', 0, 1, 1, 'Made the soup nazi very angry', 'I''m a simple who LOVES soup!')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-02-03
		Summary:	Script for inserting sample data into the UserSystemRole table.
	</summary>
*/
print '' print '*** Insert UserSystemRole Test Records ***'
GO
INSERT INTO [dbo].[UserSystemRole]
    ([UserID], [SystemRoleID])
    VALUES
    (100000, 'Admin'),
    (100001, 'User'),
    (100002, 'User'),
    (100003, 'User'),
    (100004, 'User'),
    (100005, 'User'),
    (100006, 'User'),
    (100007, 'User'),
    (100008, 'User'),
    (100009, 'User'),
    (100010, 'User'),
    (100011, 'User'),
    (100012, 'User'),
    (100013, 'User'),
    (100014, 'User'),
    (100015, 'User'),
    (100016, 'User'),
    (100017, 'User'),
    (100018, 'User')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the UserSkill table.
	</summary>
*/
print '' print '*** Insert UserSkill Test Records ***'
GO
INSERT INTO [dbo].[UserSkill]
    ([UserID], [SkillID])
VALUES
    (100000, 'Cooking'),
    (100000, 'Cleaning'),
    (100001, 'Driving'),
    (100001, 'Building'),
    (100002, 'Leadership'),
    (100002, 'Concrete work'),
    (100003, 'Backhoe'),
    (100003, 'Manual Labor'),
    (100004, 'Heavy Lifting'),
    (100004, 'Power Tools'),
    (100005, 'Light Labor'),
    (100005, 'Mowing'),
    (100006, 'Heavy Mowing'),
    (100006, 'Lawn care'),
    (100007, 'Landscaping'),
    (100007, 'Hardscaping'),
    (100008, 'Electrical'),
    (100008, 'Framing'),
    (100009, 'Plumbing'),
    (100009, 'Roofing'),
    (100010, 'Finish work'),
    (100010, 'Drywalling'),
    (100011, 'Painting'),
    (100011, 'Woodworking'),
    (100012, 'Sewing'),
    (100012, 'Crafting'),
    (100013, 'Construction Management'),
    (100013, 'Leadership'),
    (100014, 'Driving'),
    (100014, 'Manual Labor'),
    (100015, 'Plumbing'),
    (100015, 'Electrical'),
    (100016, 'Landscaping'),
    (100016, 'Roofing'),
    (100017, 'Painting'),
    (100017, 'Heavy Lifting'),
    (100018, 'Cooking'),
    (100018, 'Framing')
GO

/*
	<summary>
		Creator:	Brodie Pasker
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Notification table.
	</summary>
*/
print '' print '*** Insert Notification Test Records ***'
GO
INSERT INTO [dbo].[Notification]
    ([Name], [Sender], [Receiver], [Important], [IsViewed], [Date], [Content], [StartDate], [EndDate])
    VALUES
    ('System Maintenance',           null,      100001, 1, 0, GETDATE(), 'Scheduled system maintenance on 05/01/2025. Please save your work.',                                    GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('New Message',                  null,      100001, 0, 1, GETDATE(), 'You have received a new message from John.',                                                            GETDATE(), DATEADD(HOUR, 2, GETDATE())),
    ('Task Reminder',                100002,    100001, 0, 0, GETDATE(), 'Dont forget to submit your report by 5 PM today.',                                                      GETDATE(), DATEADD(HOUR, 8, GETDATE())),
    ('New Project Assigned',         100002,    100001, 1, 0, GETDATE(), 'You have been assigned to a new project: Park Renovation.',                                             GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Security Update',              null,      100001, 1, 1, GETDATE(), 'A new security update is available for your system. Please update as soon as possible.',                GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('Project Deadline Approaching', 100003,    100002, 0, 0, GETDATE(), 'The project deadline is approaching. Please ensure all tasks are completed by the end of the week.',    GETDATE(), DATEADD(DAY,  2, GETDATE())),
    ('Payment Reminder',             null,      100002, 0, 1, GETDATE(), 'Reminder: Your payment for the subscription is due in 3 days.',                                         GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Account Security Alert',       null,      100002, 1, 0, GETDATE(), 'Unusual login attempt detected on your account. Please verify your recent activity.',                   GETDATE(), DATEADD(HOUR, 4, GETDATE())),
    ('Meeting Scheduled',            100003,    100002, 0, 0, GETDATE(), 'A meeting has been scheduled for 03/05/2025 at 10:00 AM. Please mark your calendar.',                   GETDATE(), DATEADD(DAY,  4, GETDATE())),
    ('Holiday Announcement',         100003,    100002, 0, 1, GETDATE(), 'We are happy to announce that the company will be closed on 05/25/2025 for a holiday.',                 GETDATE(), DATEADD(DAY,  7, GETDATE())),
    ('System Maintenance',           null,      100001, 1, 0, GETDATE(), 'Scheduled system maintenance on 05/01/2025. Please save your work.',                                    GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('New Message',                  null,      100001, 0, 1, GETDATE(), 'You have received a new message from John.',                                                            GETDATE(), DATEADD(HOUR, 2, GETDATE())),
    ('Task Reminder',                100002,    100001, 0, 0, GETDATE(), 'Dont forget to submit your report by 5 PM today.',                                                      GETDATE(), DATEADD(HOUR, 8, GETDATE())),
    ('New Project Assigned',         100002,    100001, 1, 0, GETDATE(), 'You have been assigned to a new project: Park Renovation.',                                             GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Security Update',              null,      100001, 1, 1, GETDATE(), 'A new security update is available for your system. Please update as soon as possible.',                GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('Project Deadline Approaching', 100003,    100002, 0, 0, GETDATE(), 'The project deadline is approaching. Please ensure all tasks are completed by the end of the week.',    GETDATE(), DATEADD(DAY,  2, GETDATE())),
    ('Payment Reminder',             null,      100002, 0, 1, GETDATE(), 'Reminder: Your payment for the subscription is due in 3 days.',                                         GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Account Security Alert',       null,      100002, 1, 0, GETDATE(), 'Unusual login attempt detected on your account. Please verify your recent activity.',                   GETDATE(), DATEADD(HOUR, 4, GETDATE())),
    ('Meeting Scheduled',            100003,    100002, 0, 0, GETDATE(), 'A meeting has been scheduled for 03/05/2025 at 10:00 AM. Please mark your calendar.',                   GETDATE(), DATEADD(DAY,  4, GETDATE())),
    ('Holiday Announcement',         100003,    100002, 0, 1, GETDATE(), 'We are happy to announce that the company will be closed on 05/25/2025 for a holiday.',                 GETDATE(), DATEADD(DAY,  7, GETDATE())),
    ('System Maintenance',           null,      100001, 1, 0, GETDATE(), 'Scheduled system maintenance on 05/01/2025. Please save your work.',                                    GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('New Message',                  null,      100001, 0, 1, GETDATE(), 'You have received a new message from John.',                                                            GETDATE(), DATEADD(HOUR, 2, GETDATE())),
    ('Task Reminder',                100002,    100001, 0, 0, GETDATE(), 'Dont forget to submit your report by 5 PM today.',                                                      GETDATE(), DATEADD(HOUR, 8, GETDATE())),
    ('New Project Assigned',         100002,    100001, 1, 0, GETDATE(), 'You have been assigned to a new project: Park Renovation.',                                             GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Security Update',              null,      100001, 1, 1, GETDATE(), 'A new security update is available for your system. Please update as soon as possible.',                GETDATE(), DATEADD(DAY,  1, GETDATE())),
    ('Project Deadline Approaching', 100003,    100002, 0, 0, GETDATE(), 'The project deadline is approaching. Please ensure all tasks are completed by the end of the week.',    GETDATE(), DATEADD(DAY,  2, GETDATE())),
    ('Payment Reminder',             null,      100002, 0, 1, GETDATE(), 'Reminder: Your payment for the subscription is due in 3 days.',                                         GETDATE(), DATEADD(DAY,  3, GETDATE())),
    ('Account Security Alert',       null,      100002, 1, 0, GETDATE(), 'Unusual login attempt detected on your account. Please verify your recent activity.',                   GETDATE(), DATEADD(HOUR, 4, GETDATE())),
    ('Meeting Scheduled',            100003,    100002, 0, 0, GETDATE(), 'A meeting has been scheduled for 03/05/2025 at 10:00 AM. Please mark your calendar.',                   GETDATE(), DATEADD(DAY,  4, GETDATE())),
    ('Holiday Announcement',         100003,    100002, 0, 1, GETDATE(), 'We are happy to announce that the company will be closed on 05/25/2025 for a holiday.',                 GETDATE(), DATEADD(DAY,  7, GETDATE()));
GO


/*
	<summary>
	Creator: Jennifer Nicewanner, Skyann Heintz
	Created: 2025-05-01
	Summary:  Inserting a test data for the Availability table.
	</summary>
*/
print '' print '*** Insert Avaiability Test Records ***'
GO
INSERT INTO [dbo].[Availability] 
    ([UserID], [IsAvailable], [RepeatWeekly], [StartDate], [EndDate]) 
VALUES
	(100000, 1, 0, '2/22/2025 7:00:00 AM', '2/22/2025 3:00:00 AM'),
    (100000, 1, 0, '5/22/2025 7:00:00 AM', '5/23/2025 3:00:00 AM'),
    (100001, 1, 1, '5/24/2025 8:00:00 AM', '5/25/2025 6:00:00 PM'),
    (100002, 0, 0, '5/20/2025 9:00:00 AM', '5/21/2025 1:00:00 PM'),
    (100003, 1, 0, '5/23/2025 7:00:00 AM', '5/26/2025 5:00:00 PM'),
    (100004, 0, 1, '5/22/2025 7:00:00 AM', '5/28/2025 3:00:00 AM'),
    (100005, 1, 0, '5/22/2025 7:00:00 AM', '5/29/2025 3:00:00 AM'),
    (100005, 1, 0, '5/24/2025 7:00:00 AM', '5/24/2025 3:00:00 AM'),
    (100005, 1, 0, '5/25/2025 8:00:00 AM', '5/27/2025 10:00:00 PM'),
    (100006, 1, 1, '5/28/2025 6:00:00 AM', '5/30/2025 11:00:00 PM'),
    (100006, 1, 0, '5/22/2025 7:00:00 AM', '5/29/2025 3:00:00 PM'),
    (100007, 0, 1, '5/21/2025 7:00:00 AM', '5/24/2025 4:00:00 PM'),
    (100007, 1, 0, '5/22/2025 7:00:00 AM', '5/22/2025 3:00:00 PM'),
    (100007, 1, 0, '5/22/2025 7:00:00 AM', '5/22/2025 9:00:00 AM'),
    (100007, 1, 1, '5/22/2025 7:00:00 AM', '5/22/2025 3:00:00 PM'),
    (100007, 1, 0, '2/22/2025 7:00:00 AM', '2/25/2025 3:00:00 AM'),
    (100008, 1, 0, '5/27/2025 7:00:00 AM', '5/31/2025 5:00:00 PM'),
    (100009, 1, 1, '5/29/2025 6:00:00 AM', '6/01/2025 3:00:00 PM'),
    (100010, 0, 0, '5/30/2025 7:30:00 AM', '6/03/2025 4:00:00 PM'),
    (100011, 1, 1, '5/22/2025 9:00:00 AM', '5/23/2025 2:00:00 PM'),
    (100012, 0, 0, '5/24/2025 6:00:00 AM', '5/26/2025 3:00:00 AM'),
    (100013, 1, 1, '5/28/2025 7:30:00 AM', '6/02/2025 5:00:00 PM'),
    (100014, 1, 0, '5/23/2025 8:00:00 AM', '5/25/2025 6:00:00 PM'),
    (100015, 0, 1, '5/29/2025 7:00:00 AM', '5/30/2025 5:00:00 AM'),
    (100016, 1, 0, '5/22/2025 9:30:00 AM', '5/23/2025 4:00:00 PM'),
    (100017, 0, 0, '5/24/2025 7:00:00 AM', '5/28/2025 3:00:00 PM'),
    (100018, 1, 1, '5/30/2025 6:00:00 AM', '6/01/2025 12:00:00 PM')
GO

/*
	<summary>
		Creator:	Jennifer Nicewanner, Ellie Wacker
		Created:	2025-05-01
		Summary:	Insert statements for entering vehicles
	</summary>
*/
print '' print '*** Insert Vehicle Test Records ***'
GO
INSERT INTO [dbo].[Vehicle]
	([VehicleID], [UserID], [Active], [Color], [Year], [LicensePlateNumber], [InsuranceStatus], [Make], [Model], [NumberOfSeats], [TransportUtility])
VALUES
	('1HGCM82633A123457', 100001, 1, 'Red', 2022, 'ABC1234', 1, 'Toyota', 'Camry', 5, 'Personal Use'),
    ('1HGCM82633A123456', 100000, 1, 'Blue', 2022, 'ABC123', 1, 'Honda', 'Accord', 5, 'Personal'),
    ('2FTRX18W2XCA56789', 100001, 1, 'Red', 2019, 'XYZ789', 0, 'Ford', 'F-150', 5, 'Work Truck'),
    ('3GCPKREA6AG234567', 100002, 0, 'Black', 2018, 'LMN456', 1, 'Chevrolet', 'Silverado', 5, 'Commercial'),
    ('5YJSA1E26HF214890', 100003, 1, 'White', 2023, 'TES2023', 1, 'Tesla', 'Model S', 5, 'Personal'),
    ('JH4KA8260MC012345', 100004, 1, 'Silver', 2020, 'KMD678', 1, 'Acura', 'TLX', 5, 'Personal'),
    ('1C4HJXDG8LW123456', 100005, 1, 'Green', 2021, 'JEEP201', 1, 'Jeep', 'Wrangler', 5, 'Recreational'),
    ('WAUWFAFH8AN123456', 100006, 0, 'Gray', 2017, 'AUDI07', 0, 'Audi', 'A4', 5, 'Personal'),
    ('3FAFP31N7WM123456', 100007, 1, 'Yellow', 2022, 'FLX678', 1, 'Ford', 'Flex', 7, 'Family'),
    ('1GNEK13ZX3J123456', 100008, 1, 'Black', 2015, 'SUV890', 1, 'Chevrolet', 'Tahoe', 7, 'Transport'),
	('2T3DFREVXHW123456', 100008, 0, 'Blue', 2018, 'HYB001', 1, 'Toyota', 'Prius', 5, 'Eco-Friendly'),
    ('2T3ZF4DV4AW123456', 100009, 0, 'Blue', 2016, 'TOA123', 0, 'Toyota', 'RAV4', 5, 'Personal'),
    ('5UXWX9C56E0D12345', 100010, 1, 'Red', 2023, 'BMWX5', 1, 'BMW', 'X5', 5, 'Luxury'),
	('1FTFW1EG1JFA12345', 100010, 0, 'Gray', 2017, 'FOR50', 0, 'Ford', 'F-150', 5, 'Work Truck'),
	('JHLRE48739C123456', 100011, 1, 'Silver', 2021, 'HOND567', 1, 'Honda', 'Pilot', 7, 'Transport'),
    ('JHLRD7881C0123456', 100011, 1, 'White', 2021, 'HOND678', 1, 'Honda', 'CR-V', 5, 'Personal'),
    ('1FTBW2A67CKA12345', 100012, 1, 'Silver', 2020, 'TRA345', 1, 'Ford', 'Transit', 12, 'Commercial'),
    ('1HGCR2F3XFA123456', 100013, 0, 'Gray', 2019, 'SEN001', 1, 'Honda', 'Civic', 5, 'Personal'),
    ('JTDBT923061234567', 100014, 1, 'Black', 2018, 'TOYSAN', 1, 'Toyota', 'Camry', 5, 'Personal'),
    ('2C3KA63H96H123456', 100015, 0, 'Blue', 2017, 'CHRY678', 0, 'Chrysler', '300', 5, 'Luxury'),
    ('5NPE24AF5FH123456', 100016, 1, 'Green', 2023, 'HYUN123', 1, 'Hyundai', 'Sonata', 5, 'Personal'),
	('4S4BSAFC4G3123456', 100016, 1, 'Black', 2019, 'SUBARU9', 1, 'Subaru', 'Outback', 5, 'Recreational'),
    ('1C4RJFBG3LC123456', 100017, 1, 'White', 2022, 'JEEP890', 1, 'Jeep', 'Grand Cherokee', 5, 'Recreational'),
    ('4T3RFREV0EU123456', 100018, 0, 'Silver', 2021, 'HYB2021', 1, 'Toyota', 'Highlander Hybrid', 7, 'Eco-Friendly')
GO

/* 
	<summary>
	Creator:            Jennifer Nicewanner, Kate Rich, Jacob McPherson
	Created:            2025-05-01
	Summary:            Population for ExternalContact table
	</summary> 
*/
print '' print '*** Insert ExternalContact Test Records ***'
GO
INSERT INTO [dbo].[ExternalContact]
	([ExternalContactTypeID], [ContactName], [GivenName], [FamilyName], [Email], [PhoneNumber], [AddedBy], [LastUpdatedBy])
VALUES
	('Plumber', 'Plumbing And I', 'Jack', 'Horner', 'plumsandi444@gmail.com', '123-456-7890', 100001, 100001),
	('Electrician', 'Get Real Enterprises', 'Phil', 'McGraw', 'getreal123@gmail.com', '098-765-4321', 100002, 100002),
	('Builder', 'Building With Bean Dip', 'Bill', 'Ding', 'bobthebuilder999@gmail.com', '333-324-4543', 100003, 100003),
	('Plumber', 'Pipe Masters Plumbing', 'Jack', 'Horner', 'jack.horner@pipemasters.com', '123-456-7890', 100001, 100001),
	('Electrician', 'Bright Sparks Electrical', 'Emma', 'Watts', 'emma.watts@brightsparks.com', '321-654-0987', 100002, 100002),
	('Builder', 'Reliable Construction Co.', 'Michael', 'Stone', 'mstone@reliablebuilders.com', '456-789-1230', 100003, 100003),
	('Carpenter', 'Precision Woodworks', 'Olivia', 'Grain', 'olivia.grain@precisionwood.com', '567-890-2345', 100004, 100004),
	('HVAC Technician', 'Cool Breeze Solutions', 'Daniel', 'Frost', 'daniel.frost@coolbreeze.com', '555-321-6789', 100005, 100005),
	('Painter', 'Sharp Line Painting', 'Sophia', 'Brush', 'sophia.brush@sharplinepainting.com', '678-901-2345', 100006, 100006),
	('Landscaper', 'Green Thumb Landscaping', 'Lucas', 'Fern', 'lucas.fern@greenthumb.com', '789-012-3456', 100007, 100007),
	('Mechanic', 'AutoPro Repair', 'Ryan', 'Torque', 'ryan.torque@autopro.com', '890-123-4567', 100008, 100008),
	('Roofing Specialist', 'HighPoint Roofing', 'Henry', 'Shingle', 'henry.shingle@highpointroofing.com', '901-234-5678', 100009, 100009),
	('Welder', 'Iron Works Welding', 'James', 'Forge', 'james.forge@ironworks.com', '112-345-6789', 100010, 100010),
	('Mason', 'StoneCraft Masonry', 'Ethan', 'Brickman', 'ethan.brickman@stonecraft.com', '223-456-7890', 100011, 100011),
	('Locksmith', 'Secure Lock Experts', 'Madison', 'Keyes', 'madison.keyes@securelock.com', '334-567-8901', 100012, 100012),
	('Pest Control', 'Bug Free Solutions', 'David', 'Webb', 'david.webb@bugfree.com', '445-678-9012', 100013, 100013),
	('IT Technician', 'TechFix Solutions', 'Chloe', 'Bitner', 'chloe.bitner@techfix.com', '556-789-0123', 100014, 100014),
	('Glass Installer', 'Clear View Glass', 'Liam', 'Glazier', 'liam.glazier@clearview.com', '667-890-1234', 100015, 100015),
	('Surveyor', 'Landmark Surveying', 'Ava', 'Measure', 'ava.measure@landmarksurvey.com', '778-901-2345', 100016, 100016),
	('Environmental Consultant', 'EcoWise Consulting', 'Noah', 'Greenfield', 'noah.greenfield@ecowise.com', '889-012-3456', 100017, 100017),
	('Security Officer', 'SafeGuard Security', 'Isabella', 'Shield', 'isabella.shield@safeguard.com', '990-123-4567', 100018, 100018)
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner, Stan Anderson, Skyann Heintz
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Location table.
	</summary>
*/
print '' print '*** Insert Location Test Records ***'
GO
INSERT INTO [dbo].[Location]
	([Name], [Address], [City], [State], [Zip], [Country], [Description])
VALUES
	('Strickland Propane', '135 Los Gatos Road', 'Arlen', 'TX', '76001', 'USA', 'The only place to get propane and propane accessories.'),
	('Tom Landry Middle School', '123 Tom Landry Way', 'Arlen', 'TX', '76001', 'USA', 'The best middle school to learn Espanol at.'),
	('Marion City Park', '123 Big Road', 'Marion', 'Iowa', '52302', 'USA', 'Nice trees'),
	('Community Center', '123 Small Street', 'Des Moines', 'IA', '77777', 'USA', 'Big building'),
	('Second Street Park', '123 Second St', 'Cedar Rapids', 'IA', '10001', 'USA', 'Demolished city block that needs development'),
	('Goblins Cemetary', '456 Elm St', 'Shueyville', 'IA', '90001', 'USA', '1 acre cemetary'),
	('West Junior High', '789 Oak St', 'Ely', 'IA', '60601', 'USA', 'Custodian entrance is behind the school by the playground'),
	('County Hospice Home', '321 Pine St', 'Robins', 'IA', '77001', 'USA', 'New hospice development.  Has an area to the side that is planned to be a tranquility garden.'),
	('Wild Cat Den', '654 Maple St', 'Muscatine', 'IA', '33101', 'USA', 'State Park.  Many dirt, bluff trails that are difficult to negotiate.'),
	('The Meat Master', '9501 Central Avenue', 'Arlen', 'TX', '76001', 'USA', 'The local butcher shop in Arlen known for its premium meats.'),
	('Arlen Mall', '8905 W. Mall Road', 'Arlen', 'TX', '76001', 'USA', 'The shopping center in Arlen, featuring various stores and a food court.'),
	('The Tiki Hut', '7600 Arlen Blvd', 'Arlen', 'TX', '76001', 'USA', 'A tropical-themed bar where characters like Boomhauer and others sometimes hang out.'),
	('Arlen Country Club', '2450 Creekside Drive', 'Arlen', 'TX', '78921', 'USA', 'A private country club frequented by the town’s wealthy residents.'),
	('Central Park', '501 6th St', 'Coralville', 'IA', '52242', 'USA', 'Main community park in Coralville.'),
	('Mercer Park', '1317 Dover St','Iowa City', 'IA', '52240', 'USA', 'Park that has baseball diamonds.'),
	('City Park', '200 Park Road','Iowa City', 'IA', '52246', 'USA', 'The main city, community park in Iowa City.'),
	('Kirkwood', '6301 Kirkwood Blvd SW', 'Cedar Rapids', 'IA', '52404', 'USA', 'Kirkwood Community College main campus.'),
	('White House', '1600 Pennsylvania Avenue NW', 'Washington', 'DC', '20500', 'USA', 'The home of the current president.'),
	('Bass Pro Shop Pyramid', 'Bass Pro Dr', 'Memphis', 'TN', '38105', 'USA', 'The biggest Bass Pro Shop, which is shaped like a pyramid.')
GO

/*
	<summary>
		Creator:	Kate Rich, Syler Bushlack
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Project table.
	</summary>
*/
print '' print '*** Insert Project Test Records ***'
GO
INSERT INTO [dbo].[Project]
	([Name], [ProjectTypeID], [LocationID], [UserID], [StartDate], [Status], [Description], [AcceptsDonations], [PayPalAccount], [AvailableFunds])
VALUES
	('Propane Delivery to TLMS', 'Propane Emergency', 100001, 100000, '2025-05-01', 'In Progress', 'Emergency delivery of propane to Tom Landry Middle School.', 0, NULL, 500.00),
	('Make Strickland More Accessible', 'Accessibility Upgrades', 100000, 100001, '2025-05-02', 'In Progress', 'A project intended to make Strickland Propane more accessible to the handicapped population of Arlen.', 0, NULL, 0.00),
	('Community Cleanup Day', 'Cleanup', 100000, 100000, '2025-05-03', 'In Progress', 'Organizing a community cleanup event to beautify local parks.', 1, 'cleanupfunds@test.com', 133.00),
	('Food Drive for the Homeless', 'Food Drive', 100001, 100001, '2025-05-04', 'In Progress', 'Collecting and distributing food to homeless shelters.', 1, 'fooddrive@test.com', 750.00),
	('Senior Care Volunteer Program', 'Senior Assistance', 100001, 100002, '2025-05-05', 'In Progress', 'Providing companionship and assistance to elderly residents.', 0, NULL, 325.00),
	('Youth Mentorship Initiative', 'Youth Mentorship Program', 100002, 100003, '2025-05-06', 'In Progress', 'Mentoring underprivileged youth to help them achieve their goals.', 1, 'mentorshipfund@test.com', 200.00),
	('Community Garden Development', 'Community Garden', 100003, 100004, '2025-05-07', 'In Progress', 'Creating a community garden to promote sustainable living.', 1, 'gardenfund@test.com', 1302.00),
	('Literacy Program for Adults', 'Community Event Support', 100000, 100005, '2025-05-08', 'In Progress', 'Teaching adults basic literacy and numeracy skills.', 0, NULL, 0.00),
	('Disaster Relief Support', 'Disaster Relief', 100000, 100006, '2025-05-09', 'In Progress', 'Providing aid and support to communities affected by natural disasters.', 1, 'disasterrelief@test.com', 3000.00),
	('Annual Propane Safety Seminar', 'Community Event Support', 100003, 100000, '2025-05-10', 'In Progress', 'Hank Hill leads a propane safety seminar for local residents.', 0, NULL, 300.00),
	('Strickland Propane Warehouse Overhaul', 'Accessibility Upgrades', 100000, 100000, '2025-05-11', 'In Progress', 'Overhaul of Strickland Propane’s main warehouse to upgrade equipment and safety protocols.', 1, 'propaneoverhaul@test.com', 5000.00),
	('Arlen Country Club Fundraiser', 'Community Event Support', 100005, 100001, '2025-05-12', 'In Progress', 'A fundraiser at the Arlen Country Club to support local youth programs and charities.', 1, 'clubfundraiser@test.com', 1500.00),
	('Arlen Middle School Propane Classroom', 'School Supply Drive', 100001, 100001, '2025-05-13', 'In Progress', 'Installation of a propane-powered classroom in Arlen High School for vocational training.', 0, NULL, 275.00),
	('HVAC System Upgrade', 'Home Repair Assistance', 100002, 100000, '2025-05-14', 'In Progress', 'Upgrade of the Tom Landry Middle School’s HVAC system, with propane-powered backup systems.', 1, 'hvacupgrade@test.com', 6000.00),
	('BBQ Propane Setup', 'Propane Emergency', 100004, 100000, '2025-05-15', 'Complete', 'Installation of a propane-powered BBQ setup at The Tiki Hut.', 0, NULL, 0.00),
	('Hank''s Propane Customer Appreciation Day', 'Community Event Support', 100000, 100000, '2025-05-16', 'In Progress', 'Hank Hill organizes a customer appreciation day at Strickland Propane to celebrate loyal customers.', 1, 'customerappreciation@test.com', 425.00),
	('Community Propane Stockpile', 'Propane Emergency', 100001, 100000, '2025-05-17', 'In Progress', 'Contribute to community propane stockpile.', 1, 'test@test.com', 2500.00),
	('Build a Park', 'Improvements', 100000, 100001, '2025-05-18', 'In Progress', 'A project to raise money to build a new park.', 1, 'test2@test.com', 4000.00)
GO


/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Event table.
	</summary>
*/
print '' print '*** Insert Event Test Records ***'
GO
INSERT INTO [dbo].[Event]
	([EventTypeID], [ProjectID], [DateCreated], [StartDate], [EndDate], [Name], [LocationID], [VolunteersNeeded], [UserID], [Notes], [Description])
VALUES
    ('Conference', 100000, '2025-05-01', '2025-05-10', '2025-05-10', 'Propane Safety Conference', 100001, 50, 100000, 'Educational sessions on propane safety.', 'Conference focused on best practices for propane delivery and emergency response.'),
    ('Workshop', 100001, '2025-05-02', '2025-05-12', '2025-05-12', 'Accessibility Design Workshop', 100000, 30, 100001, 'Interactive workshop on accessibility improvements.', 'A hands-on session exploring ways to make Strickland Propane more accessible.'),
    ('Seminar', 100002, '2025-05-03', '2025-05-15', '2025-05-15', 'Community Park Cleanup', 100000, 100, 100002, 'Bring gloves and bags!', 'A cleanup initiative to beautify local parks and green areas.'),
    ('Webinar', 100003, '2025-05-04', '2025-05-20', '2025-05-20', 'Food Drive Fundraising Gala', 100001, 75, 100003, 'Charity dinner to raise funds.', 'An event supporting local food banks and shelters through donations and fundraising.'),
    ('Fundraiser', 100004, '2025-05-05', '2025-05-25', '2025-05-25', 'Senior Volunteer Appreciation Event', 100001, 20, 100004, 'Celebration with light refreshments.', 'A gathering to thank volunteers who assist elderly residents.'),
    ('Team Meeting', 100005, '2025-05-06', '2025-05-18', '2025-05-18', 'Youth Leadership Training', 100002, 40, 100005, 'Skill-building activities.', 'A mentorship workshop aimed at empowering underprivileged youth.'),
    ('Networking Event', 100006, '2025-05-07', '2025-05-22', '2025-05-22', 'Community Garden Expansion Discussion', 100003, 25, 100006, 'Stakeholder meeting.', 'A networking event to plan the next phase of the community garden project.'),
    ('Open House', 100007, '2025-05-08', '2025-05-28', '2025-05-28', 'Adult Literacy Coaching', 100000, 20, 100007, 'Teaching strategies for literacy.', 'Volunteers learn effective methods for adult literacy tutoring.'),
    ('Award Ceremony', 100008, '2025-05-09', '2025-06-03', '2025-06-03', 'Disaster Relief Coordination Meeting', 100000, 100, 100008, 'Community preparedness discussion.', 'A town hall for planning disaster relief efforts and mobilizing volunteers.'),
    ('Training Session', 100009, '2025-05-10', '2025-06-10', '2025-06-10', 'Propane Handling Seminar', 100003, 60, 100009, 'Best practices for propane use.', 'Training seminar focused on safe propane handling techniques.'),
    ('Social Gathering', 100010, '2025-05-11', '2025-06-15', '2025-06-15', 'Propane Warehouse Overhaul Panel', 100000, 40, 100010, 'Industry discussion.', 'Expert-led discussion on upgrading equipment and safety protocols at Strickland Propane.'),
    ('Charity Event', 100011, '2025-05-12', '2025-06-20', '2025-06-20', 'Youth Charity Golf Tournament', 100005, 75, 100011, 'Golf event supporting youth programs.', 'A charity event raising funds for local educational initiatives.'),
    ('Panel Discussion', 100012, '2025-05-13', '2025-06-25', '2025-06-25', 'Vocational Propane Training', 100001, 30, 100012, 'Hands-on propane training.', 'A workshop on propane-powered tools and classroom applications for vocational students.'),
    ('Recruitment Fair', 100013, '2025-05-14', '2025-07-01', '2025-07-01', 'HVAC Upgrade Info Session', 100002, 50, 100013, 'Public discussion on new HVAC systems.', 'A Q&A event for Tom Landry Middle School parents about the new propane-powered HVAC upgrades.'),
    ('Strategy Meeting', 100014, '2025-05-15', '2025-07-05', '2025-07-05', 'BBQ Propane Setup Showcase', 100004, 30, 100014, 'Demo event of the new BBQ installation.', 'A showcase event celebrating the propane-powered BBQ setup at The Tiki Hut.'),
    ('Exhibition', 100015, '2025-05-16', '2025-07-10', '2025-07-10', 'Customer Appreciation Day', 100000, 100, 100015, 'Celebration with giveaways.', 'Hank Hill hosts a Strickland Propane appreciation day for loyal customers.'),
    ('Town Hall', 100016, '2025-05-17', '2025-07-15', '2025-07-15', 'Community Propane Stockpile Donation Drive', 100001, 200, 100016, 'Accepting propane donations.', 'An effort to gather propane contributions for community emergency reserves.'),
    ('Q and A Session', 100017, '2025-05-18', '2025-07-20', '2025-07-20', 'New Park Vision Open House', 100000, 75, 100017, 'Public planning session.', 'An open house event showcasing the plans for a new community park.')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the NeedList table.
	</summary>
*/
print '' print '*** Insert NeedList Test Records ***'
GO
INSERT INTO [dbo].[Needlist]
	([ProjectID], [Name], [Quantity], [Price], [Description], [IsObtained])
VALUES
	(100000, 'Propane Tanks', 10, 50.00, 'Propane tanks for emergency delivery at Tom Landry Middle School.', 0),
    (100001, 'Wheelchair Ramp Materials', 5, 200.00, 'Wood, screws, and metal for building accessibility ramps.', 0),
    (100002, 'Trash Bags', 100, 10.00, 'Heavy-duty trash bags for the community cleanup event.', 1),
    (100003, 'Canned Goods', 500, 2.50, 'Non-perishable food items for the food drive.', 1),
    (100004, 'Chairs', 20, 30.00, 'Folding chairs for senior volunteer gatherings.', 0),
    (100005, 'Notebooks', 50, 5.00, 'Journals and notebooks for youth mentorship activities.', 1),
    (100006, 'Gardening Tools', 25, 15.00, 'Shovels, gloves, and rakes for community garden maintenance.', 0),
    (100007, 'Whiteboards', 10, 40.00, 'Whiteboards for literacy training sessions.', 0),
    (100008, 'First Aid Kits', 15, 25.00, 'Medical kits for disaster relief and emergency support.', 1),
    (100009, 'Safety Goggles', 30, 10.00, 'Protective eyewear for propane handling workshops.', 0),
    (100010, 'Warehouse Shelving', 5, 500.00, 'New storage racks for Strickland Propane’s warehouse.', 0),
    (100011, 'Golf Event Supplies', 100, 15.00, 'Golf balls, tees, and scorecards for the charity tournament.', 1),
    (100012, 'Training Manual Copies', 200, 8.00, 'Printed training manuals for propane vocational classes.', 0),
    (100013, 'HVAC Filters', 50, 30.00, 'Replacement filters for the propane-powered HVAC system.', 1),
    (100014, 'Grill Grates', 15, 20.00, 'New grill components for the propane BBQ installation.', 0),
    (100015, 'Gift Bags', 50, 12.00, 'Promotional gifts for Strickland Propane’s customer appreciation day.', 1),
    (100016, 'Propane Storage Containers', 8, 120.00, 'Containers for storing emergency propane stockpile.', 0),
    (100017, 'Park Benches', 10, 250.00, 'New benches for the community park project.', 0)
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Document table.
	</summary>
*/
print '' print '*** Insert Document Test Records ***'
GO
INSERT INTO [dbo].[Document]
	([DocumentTypeID], [ReferenceID], [FileName], [FileType], [Artifact], [Uploader], [Description])
VALUES
	('User', 100002, 'background_check.docx', 'DOCX', NULL, 100002, 'Background check report for volunteer processing.'),
    ('User', 100005, 'certification.pdf', 'PDF', NULL, 100005, 'Volunteer training certification document.'),
    ('Project', 100010, 'building_permit.pdf', 'PDF', NULL, 100010, 'Permit for warehouse overhaul construction.'),
    ('Project', 100012, 'inspection_report.doc', 'DOC', NULL, 100012, 'Safety inspection report for vocational propane training center.'),
    ('User', 100008, 'volunteer_contract.pdf', 'PDF', NULL, 100008, 'Signed contract for disaster relief volunteer commitment.'),
    ('Project', 100014, 'event_plan.xlsx', 'XLSX', NULL, 100014, 'Detailed event planning document for BBQ propane setup showcase.'),
    ('Project', 100017, 'park_blueprints.dwg', 'DWG', NULL, 100017, 'Blueprints for the new community park construction.'),
    ('User', 100010, 'liability_waiver.pdf', 'PDF', NULL, 100010, 'Signed liability waiver for community cleanup event.'),
    ('Project', 100005, 'mentorship_guidelines.doc', 'DOC', NULL, 100005, 'Guidelines for youth mentorship initiative.'),
    ('Project', 100007, 'garden_expansion_plan.ppt', 'PPT', NULL, 100007, 'Community garden expansion proposal.'),
    ('User', 100012, 'senior_assistance_policy.pdf', 'PDF', NULL, 100012, 'Official policy document on senior care volunteer program.'),
    ('Project', 100016, 'fundraiser_budget.xlsx', 'XLSX', NULL, 100016, 'Budget allocation for the propane stockpile donation drive.')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the BackgroundCheck table.
	</summary>
*/
print '' print '*** Insert BackgroundCheck Test Records ***'
GO
INSERT INTO [dbo].[BackgroundCheck]
	([Investigator], [UserID], [ProjectID])
VALUES
	(100000, 100007, 100000),
    (100001, 100008, 100001),
    (100000, 100007, 100002),
    (100001, 100009, 100004),
    (100001, 100009, 100005),
    (100002, 100010, 100006),
    (100003, 100010, 100007),
    (100004, 100011, 100008),
    (100005, 100011, 100009),
    (100000, 100012, 100010),
    (100000, 100012, 100011),
    (100001, 100013, 100012),
    (100001, 100013, 100013),
    (100000, 100014, 100014),
    (100000, 100014, 100015),
    (100001, 100015, 100016),
    (100000, 100016, 100017),
    (100000, 100017, 100000),
    (100001, 100018, 100001)
GO

/* 
	<summary>
	Creator:            Syler Bushlack, Jennifer Nicewanner
	Created:            2025-05-01
	Summary:            Creation of test data of pending join project requests for VolunteerStatus table.
	</summary> 
*/
print '' print '*** Insert VolunteerStatus Test Records ***'
GO
INSERT INTO [dbo].[VolunteerStatus]
	([UserID], [ProjectID], [Approved])
VALUES
	-- Project Starters
	(100000, 100000, 1),
	(100000, 100002, 1),
	(100000, 100003, 1),
	(100000, 100006, 1),
	(100000, 100009, 1),
	(100000, 100010, 1),
	(100000, 100011, 1),
	(100000, 100014, 1),
	(100000, 100015, 1),
	(100000, 100016, 1),
	(100000, 100017, 1),
	
	(100001, 100001, 1),
	(100001, 100004, 1),
	(100001, 100005, 1),
	(100001, 100012, 1),
	(100001, 100013, 1),
	
	(100002, 100006, 1),
	
	(100003, 100007, 1),
	
	(100004, 100008, 1),
    
	(100005, 100009, 1),
	
	(100006, 100010, 1),
	
	(100008, 100001, 1),

	-- Volunteers Applying
	(100007, 100000, 1),
    (100007, 100002, 0),
    (100008, 100003, NULL),
    (100009, 100004, 0),
    (100009, 100005, 1),
    (100010, 100006, NULL),
    (100010, 100007, 0),
    (100011, 100008, 1),
    (100011, 100009, NULL),
    (100012, 100010, 1),
    (100012, 100011, 0),
    (100013, 100012, NULL),
    (100013, 100013, 1),
    (100014, 100014, 0),
    (100014, 100015, NULL),
    (100015, 100016, 1),
    (100016, 100017, 0),
    (100017, 100000, NULL),
    (100018, 100001, 1)
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the VolunteerStatusProjectRole table.
	</summary>
*/
print '' print '*** Insert VolunteerStatusProjectRole Test Records ***'
GO
INSERT INTO [dbo].[VolunteerStatusProjectRole]
	([UserID], [ProjectID], [ProjectRoleID])
VALUES
	(100000, 100000, 'Volunteer Director'),
    (100000, 100000, 'Project Starter'),
    (100000, 100000, 'Volunteer'),
    (100000, 100000, 'Background Checker'),
    (100000, 100000, 'Moderator'),
    (100000, 100000, 'Accountant'),
    (100000, 100000, 'Purchaser'),
    (100000, 100000, 'Scheduler'),
    (100000, 100000, 'Driver'),
    (100000, 100000, 'Event Coordinator'),
	
	(100000, 100002, 'Volunteer Director'),
    (100000, 100002, 'Project Starter'),
    (100000, 100002, 'Volunteer'),
    (100000, 100002, 'Background Checker'),
    (100000, 100002, 'Moderator'),
    (100000, 100002, 'Accountant'),
    (100000, 100002, 'Purchaser'),
    (100000, 100002, 'Scheduler'),
    (100000, 100002, 'Driver'),
    (100000, 100002, 'Event Coordinator'),
	
	(100000, 100003, 'Volunteer Director'),
    (100000, 100003, 'Project Starter'),
    (100000, 100003, 'Volunteer'),
    (100000, 100003, 'Background Checker'),
    (100000, 100003, 'Moderator'),
    (100000, 100003, 'Accountant'),
    (100000, 100003, 'Purchaser'),
    (100000, 100003, 'Scheduler'),
    (100000, 100003, 'Driver'),
    (100000, 100003, 'Event Coordinator'),
	
	(100000, 100006, 'Volunteer Director'),
    (100000, 100006, 'Project Starter'),
    (100000, 100006, 'Volunteer'),
    (100000, 100006, 'Background Checker'),
    (100000, 100006, 'Moderator'),
    (100000, 100006, 'Accountant'),
    (100000, 100006, 'Purchaser'),
    (100000, 100006, 'Scheduler'),
    (100000, 100006, 'Driver'),
    (100000, 100006, 'Event Coordinator'),
	
	(100000, 100009, 'Volunteer Director'),
    (100000, 100009, 'Project Starter'),
    (100000, 100009, 'Volunteer'),
    (100000, 100009, 'Background Checker'),
    (100000, 100009, 'Moderator'),
    (100000, 100009, 'Accountant'),
    (100000, 100009, 'Purchaser'),
    (100000, 100009, 'Scheduler'),
    (100000, 100009, 'Driver'),
    (100000, 100009, 'Event Coordinator'),
	
	(100000, 100010, 'Volunteer Director'),
    (100000, 100010, 'Project Starter'),
    (100000, 100010, 'Volunteer'),
    (100000, 100010, 'Background Checker'),
    (100000, 100010, 'Moderator'),
    (100000, 100010, 'Accountant'),
    (100000, 100010, 'Purchaser'),
    (100000, 100010, 'Scheduler'),
    (100000, 100010, 'Driver'),
    (100000, 100010, 'Event Coordinator'),
	
	(100000, 100011, 'Volunteer Director'),
    (100000, 100011, 'Project Starter'),
    (100000, 100011, 'Volunteer'),
    (100000, 100011, 'Background Checker'),
    (100000, 100011, 'Moderator'),
    (100000, 100011, 'Accountant'),
    (100000, 100011, 'Purchaser'),
    (100000, 100011, 'Scheduler'),
    (100000, 100011, 'Driver'),
    (100000, 100011, 'Event Coordinator'),
	
	(100000, 100014, 'Volunteer Director'),
    (100000, 100014, 'Project Starter'),
    (100000, 100014, 'Volunteer'),
    (100000, 100014, 'Background Checker'),
    (100000, 100014, 'Moderator'),
    (100000, 100014, 'Accountant'),
    (100000, 100014, 'Purchaser'),
    (100000, 100014, 'Scheduler'),
    (100000, 100014, 'Driver'),
    (100000, 100014, 'Event Coordinator'),
	
	(100000, 100015, 'Volunteer Director'),
    (100000, 100015, 'Project Starter'),
    (100000, 100015, 'Volunteer'),
    (100000, 100015, 'Background Checker'),
    (100000, 100015, 'Moderator'),
    (100000, 100015, 'Accountant'),
    (100000, 100015, 'Purchaser'),
    (100000, 100015, 'Scheduler'),
    (100000, 100015, 'Driver'),
    (100000, 100015, 'Event Coordinator'),
	
	(100000, 100016, 'Volunteer Director'),
    (100000, 100016, 'Project Starter'),
    (100000, 100016, 'Volunteer'),
    (100000, 100016, 'Background Checker'),
    (100000, 100016, 'Moderator'),
    (100000, 100016, 'Accountant'),
    (100000, 100016, 'Purchaser'),
    (100000, 100016, 'Scheduler'),
    (100000, 100016, 'Driver'),
    (100000, 100016, 'Event Coordinator'),
	
	(100000, 100017, 'Volunteer Director'),
    (100000, 100017, 'Project Starter'),
    (100000, 100017, 'Volunteer'),
    (100000, 100017, 'Background Checker'),
    (100000, 100017, 'Moderator'),
    (100000, 100017, 'Accountant'),
    (100000, 100017, 'Purchaser'),
    (100000, 100017, 'Scheduler'),
    (100000, 100017, 'Driver'),
    (100000, 100017, 'Event Coordinator'),
	
	(100001, 100001, 'Volunteer Director'),
    (100001, 100001, 'Project Starter'),
    (100001, 100001, 'Volunteer'),
    (100001, 100001, 'Background Checker'),
    (100001, 100001, 'Moderator'),
    (100001, 100001, 'Accountant'),
    (100001, 100001, 'Purchaser'),
    (100001, 100001, 'Scheduler'),
    (100001, 100001, 'Driver'),
    (100001, 100001, 'Event Coordinator'),
	
	(100001, 100004, 'Volunteer Director'),
    (100001, 100004, 'Project Starter'),
    (100001, 100004, 'Volunteer'),
    (100001, 100004, 'Background Checker'),
    (100001, 100004, 'Moderator'),
    (100001, 100004, 'Accountant'),
    (100001, 100004, 'Purchaser'),
    (100001, 100004, 'Scheduler'),
    (100001, 100004, 'Driver'),
    (100001, 100004, 'Event Coordinator'),
	
	(100001, 100005, 'Volunteer Director'),
    (100001, 100005, 'Project Starter'),
    (100001, 100005, 'Volunteer'),
    (100001, 100005, 'Background Checker'),
    (100001, 100005, 'Moderator'),
    (100001, 100005, 'Accountant'),
    (100001, 100005, 'Purchaser'),
    (100001, 100005, 'Scheduler'),
    (100001, 100005, 'Driver'),
    (100001, 100005, 'Event Coordinator'),
	
	(100001, 100012, 'Volunteer Director'),
    (100001, 100012, 'Project Starter'),
    (100001, 100012, 'Volunteer'),
    (100001, 100012, 'Background Checker'),
    (100001, 100012, 'Moderator'),
    (100001, 100012, 'Accountant'),
    (100001, 100012, 'Purchaser'),
    (100001, 100012, 'Scheduler'),
    (100001, 100012, 'Driver'),
    (100001, 100012, 'Event Coordinator'),
	
	(100001, 100013, 'Volunteer Director'),
    (100001, 100013, 'Project Starter'),
    (100001, 100013, 'Volunteer'),
    (100001, 100013, 'Background Checker'),
    (100001, 100013, 'Moderator'),
    (100001, 100013, 'Accountant'),
    (100001, 100013, 'Purchaser'),
    (100001, 100013, 'Scheduler'),
    (100001, 100013, 'Driver'),
    (100001, 100013, 'Event Coordinator'),
	
	(100002, 100006, 'Volunteer Director'),
    (100002, 100006, 'Project Starter'),
    (100002, 100006, 'Volunteer'),
    (100002, 100006, 'Background Checker'),
    (100002, 100006, 'Moderator'),
    (100002, 100006, 'Accountant'),
    (100002, 100006, 'Purchaser'),
    (100002, 100006, 'Scheduler'),
    (100002, 100006, 'Driver'),
    (100002, 100006, 'Event Coordinator'),
	
	(100003, 100007, 'Volunteer Director'),
    (100003, 100007, 'Project Starter'),
    (100003, 100007, 'Volunteer'),
    (100003, 100007, 'Background Checker'),
    (100003, 100007, 'Moderator'),
    (100003, 100007, 'Accountant'),
    (100003, 100007, 'Purchaser'),
    (100003, 100007, 'Scheduler'),
    (100003, 100007, 'Driver'),
    (100003, 100007, 'Event Coordinator'),
	
	(100004, 100008, 'Volunteer Director'),
    (100004, 100008, 'Project Starter'),
    (100004, 100008, 'Volunteer'),
    (100004, 100008, 'Background Checker'),
    (100004, 100008, 'Moderator'),
    (100004, 100008, 'Accountant'),
    (100004, 100008, 'Purchaser'),
    (100004, 100008, 'Scheduler'),
    (100004, 100008, 'Driver'),
    (100004, 100008, 'Event Coordinator'),
	
	(100005, 100009, 'Volunteer Director'),
    (100005, 100009, 'Project Starter'),
    (100005, 100009, 'Volunteer'),
    (100005, 100009, 'Background Checker'),
    (100005, 100009, 'Moderator'),
    (100005, 100009, 'Accountant'),
    (100005, 100009, 'Purchaser'),
    (100005, 100009, 'Scheduler'),
    (100005, 100009, 'Driver'),
    (100005, 100009, 'Event Coordinator'),
	
	(100006, 100010, 'Volunteer Director'),
    (100006, 100010, 'Project Starter'),
    (100006, 100010, 'Volunteer'),
    (100006, 100010, 'Background Checker'),
    (100006, 100010, 'Moderator'),
    (100006, 100010, 'Accountant'),
    (100006, 100010, 'Purchaser'),
    (100006, 100010, 'Scheduler'),
    (100006, 100010, 'Driver'),
    (100006, 100010, 'Event Coordinator')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the ForumPermission table.
	</summary>
*/
print '' print '*** Insert ForumPermission Test Records ***'
GO
INSERT INTO [dbo].[ForumPermission] 
    ([UserID], [ProjectID], [WriteAccess])
VALUES 
	-- Project Starters
	(100000, 100000, 1),
	(100000, 100002, 1),
	(100000, 100003, 1),
	(100000, 100006, 1),
	(100000, 100009, 1),
	(100000, 100010, 1),
	(100000, 100011, 1),
	(100000, 100014, 1),
	(100000, 100015, 1),
	(100000, 100016, 1),
	(100000, 100017, 1),
	
	(100001, 100001, 1),
	(100001, 100004, 1),
	(100001, 100005, 1),
	(100001, 100012, 1),
	(100001, 100013, 1),
	
	(100002, 100006, 1),
	
	(100003, 100007, 1),
	
	(100004, 100008, 1),
    
	(100005, 100009, 1),
	
	(100006, 100010, 1),
	
	(100008, 100001, 1),
	
	-- Approved Volunteers
	(100009, 100005, 1),
	(100011, 100008, 1),
	(100012, 100010, 1),
	(100013, 100013, 1),
	(100015, 100016, 1),
	(100018, 100001, 1)
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Thread table.
	</summary>
*/
print '' print '*** Insert Thread Test Records ***'
GO
INSERT INTO [dbo].[Thread]
	([ProjectID], [UserID], [ThreadName], [DatePosted])
VALUES
    (100000, 100000, 'Welcome to Propane Delivery to TLMS!', '2025-05-01'),
    (100001, 100001, 'Welcome to Make Strickland More Accessible!', '2025-05-02'),
    (100002, 100000, 'Welcome to Community Cleanup Day!', '2025-05-03'),
    (100003, 100000, 'Welcome to Food Drive for the Homeless!', '2025-05-04'),
    (100004, 100001, 'Welcome to Senior Care Volunteer Program!', '2025-05-05'),
    (100005, 100001, 'Welcome to Youth Mentorship Initiative!', '2025-05-06'),
    (100006, 100002, 'Welcome to Community Garden Development!', '2025-05-07'),
    (100007, 100003, 'Welcome to Literacy Program for Adults!', '2025-05-08'),
    (100008, 100004, 'Welcome to Disaster Relief Support!', '2025-05-09'),
    (100009, 100005, 'Welcome to Annual Propane Safety Seminar!', '2025-05-10'),
    (100010, 100006, 'Welcome to Strickland Propane Warehouse Overhaul!', '2025-05-11'),
    (100011, 100000, 'Welcome to Arlen Country Club Fundraiser!', '2025-05-12'),
    (100012, 100001, 'Welcome to Arlen Middle School Propane Classroom!', '2025-05-13'),
    (100013, 100001, 'Welcome to HVAC System Upgrade!', '2025-05-14'),
    (100014, 100000, 'Welcome to BBQ Propane Setup!', '2025-05-15'),
    (100015, 100000, 'Welcome to Hank''s Propane Customer Appreciation Day!', '2025-05-16'),
    (100016, 100000, 'Welcome to Community Propane Stockpile!', '2025-05-17'),
    (100017, 100000, 'Welcome to Build a Park!', '2025-05-18')
GO


/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Post table.
	</summary>
*/
print '' print '*** Insert Post Test Records ***'
GO
INSERT INTO [dbo].[Post]
	([ThreadID], [UserID], [Reply], [Edited], [Pinned], [Content], [DatePosted])
VALUES
	(100000, 100000, 0, 0, 1, 'Excited to get started on propane deliveries! Let''s work together to make this happen.', '2025-05-01'),
    (100001, 100001, 0, 0, 1, 'Making Strickland more accessible is key for everyone in our community!', '2025-05-02'),
    (100002, 100000, 0, 0, 1, 'Join us for a cleanup day to keep our parks beautiful!', '2025-05-03'),
    (100003, 100000, 0, 0, 1, 'Together, we can help feed those in need!', '2025-05-04'),
    (100004, 100001, 0, 0, 1, 'Helping seniors is something we all can be a part of.', '2025-05-05'),
    (100005, 100001, 0, 0, 1, 'Our mentorship program will help change lives!', '2025-05-06'),
    (100006, 100002, 0, 0, 1, 'Let''s bring new life to our community gardens!', '2025-05-07'),
    (100007, 100003, 0, 0, 1, 'Improving literacy helps build stronger futures!', '2025-05-08'),
    (100008, 100004, 0, 0, 1, 'Together, we can help communities recover from disasters!', '2025-05-09'),
    (100009, 100005, 0, 0, 1, 'Safety first when it comes to propane!', '2025-05-10'),
    (100010, 100006, 0, 0, 1, 'Time to overhaul and improve Strickland Propane!', '2025-05-11'),
    (100011, 100000, 0, 0, 1, 'Join our fundraiser at Arlen Country Club for local youth programs!', '2025-05-12'),
    (100012, 100001, 0, 0, 1, 'A propane classroom will give students hands-on experience!', '2025-05-13'),
    (100013, 100001, 0, 0, 1, 'Upgrading HVAC systems will improve efficiency and safety!', '2025-05-14'),
    (100014, 100000, 0, 0, 1, 'Join us to celebrate our new propane BBQ setup!', '2025-05-15'),
    (100015, 100000, 0, 0, 1, 'Customer appreciation day is here! Thank you for supporting Strickland Propane!', '2025-05-16'),
    (100016, 100000, 0, 0, 1, 'Stockpiling propane for emergencies will help the whole community!', '2025-05-17'),
    (100017, 100000, 0, 0, 1, 'A new park means new opportunities for everyone!', '2025-05-18')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Task table.
	</summary>
*/
print '' print '*** Insert Task Test Records ***'
GO
INSERT INTO [dbo].[Task] 
	([Name], [Description], [TaskDate], [ProjectID], [TaskType], [EventID])
VALUES 
	('Unload Propane Tanks', 'Move propane tanks safely to designated storage areas.', '2025-05-01', 100000, 'Heavy Lifting', 100000),
	('Ramp Construction', 'Build accessible ramps for Strickland Propane.', '2025-05-02', 100001, 'Construction', 100001),
	('Park Cleanup', 'Remove trash and debris from the local park.', '2025-05-03', 100002, 'Cleanup', NULL),
	('Food Packing', 'Organize and pack canned goods for distribution.', '2025-05-04', 100003, 'Food Prep and Distribution', 100003),
	('Senior Assistance Transport', 'Help seniors with transportation to the community center.', '2025-05-05', 100004, 'Logistics', NULL),
	('Youth Leadership Seminar Setup', 'Arrange seating and materials for the event.', '2025-05-06', 100005, 'Setup', 100005),
	('Garden Bed Preparation', 'Dig and prepare soil for community planting.', '2025-05-07', 100006, 'Digging', NULL),
	('Literacy Program Materials', 'Organize books and learning materials.', '2025-05-08', 100007, 'Shelving and Stocking', 100007),
	('Disaster Relief Packing', 'Prepare emergency relief kits for distribution.', '2025-05-09', 100008, 'Warehouse Work', NULL),
	('Safety Seminar Speaker Setup', 'Ensure microphones and speaker system are functional.', '2025-05-10', 100009, 'Tech Support', 100009),
	('Warehouse Shelving Installation', 'Install new shelves for better storage.', '2025-05-11', 100010, 'Storage Organization', NULL),
	('Fundraiser Ticket Registration', 'Manage attendee check-in and donation processing.', '2025-05-12', 100011, 'Registration', 100011),
	('Classroom Electrical Wiring', 'Install propane-powered electrical setup for learning space.', '2025-05-13', 100012, 'Electrical Work', NULL),
	('HVAC System Upgrade', 'Install new propane-based heating and cooling units.', '2025-05-14', 100013, 'Roofing and Repairs', 100013),
	('BBQ Station Assembly', 'Set up propane grills and cooking areas.', '2025-05-15', 100014, 'Assembly', 100014),
	('Customer Appreciation Event Cleanup', 'Restore the venue after the celebration.', '2025-05-16', 100015, 'Cleanup', NULL),
	('Propane Stockpile Management', 'Organize and secure propane supply reserves.', '2025-05-17', 100016, 'Storage Organization', NULL),
	('Community Park Planting', 'Plant trees and shrubs in the new park.', '2025-05-18', 100017, 'Landscaping', 100017)
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the TaskAssigned table.
	</summary>
*/
print '' print '*** Insert TaskAssigned Test Records ***'
GO
INSERT INTO [dbo].[TaskAssigned] 
	([UserID], [TaskID])
VALUES 
	(100007, 100000),
	(100008, 100001),
	(100009, 100005),
	(100011, 100008),
	(100012, 100010),
	(100013, 100013),
	(100015, 100016)

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Expense table.
	</summary>
*/
print '' print '*** Insert Expense Test Records ***'
GO
INSERT INTO [dbo].[Expense] 
	([ProjectID], [ExpenseTypeID], [Date], [Amount], [Description])
VALUES
	(100000, 'Insurance', '2025-05-01', 500.00, 'Liability coverage for propane delivery project.'),
	(100001, 'Rent/Utilities', '2025-05-02', 1200.00, 'Utility upgrades for accessibility improvements at Strickland Propane.'),
	(100002, 'Marketing/Advertising', '2025-05-03', 300.00, 'Flyers and social media ads for community cleanup event.'),
	(100003, 'Other', '2025-05-04', 250.00, 'Miscellaneous costs for food drive logistics.'),
	(100004, 'Travel/Entertainment', '2025-05-05', 400.00, 'Transportation reimbursement for senior care volunteers.'),
	(100005, 'Accounting/Legal Fees', '2025-05-06', 600.00, 'Legal assistance for youth mentorship agreements.'),
	(100006, 'Bank Fees', '2025-05-07', 75.00, 'Processing fees for community garden donations.'),
	(100007, 'Rent/Utilities', '2025-05-08', 900.00, 'Electricity costs for literacy program facilities.'),
	(100008, 'Insurance', '2025-05-09', 550.00, 'Coverage for disaster relief materials and workers.'),
	(100009, 'Marketing/Advertising', '2025-05-10', 350.00, 'Promotion efforts for propane safety seminar.'),
	(100010, 'Travel/Entertainment', '2025-05-11', 200.00, 'Travel expenses for warehouse improvement team.'),
	(100011, 'Accounting/Legal Fees', '2025-05-12', 500.00, 'Fundraiser legal compliance and tax documentation.'),
	(100012, 'Other', '2025-05-13', 275.00, 'Material testing for propane classroom construction.'),
	(100013, 'Rent/Utilities', '2025-05-14', 1500.00, 'HVAC system energy cost projections.'),
	(100014, 'Bank Fees', '2025-05-15', 100.00, 'Transaction fees from BBQ event donations.'),
	(100015, 'Insurance', '2025-05-16', 450.00, 'Event coverage for propane customer appreciation day.'),
	(100016, 'Marketing/Advertising', '2025-05-17', 425.00, 'Community awareness campaign for propane stockpile effort.'),
	(100017, 'Other', '2025-05-18', 325.00, 'Miscellaneous costs for park development planning.')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Invoice table.
	</summary>
*/
print '' print '*** Insert Invoice Test Records ***'
GO
INSERT INTO [dbo].[Invoice] 
	([InvoiceNumber], [ExpenseID], [ProjectID], [InvoiceDate], [Status], [Description])
VALUES
	('INV-100000', 100000, 100000, '2025-05-01', 'Paid', 'Insurance payment for propane delivery project.'),
	('INV-100001', NULL, 100001, '2025-05-02', 'Pending', 'Utility expenses for accessibility improvements.'),
	('INV-100002', 100002, 100002, '2025-05-03', 'Processed', 'Marketing costs for community cleanup event.'),
	('INV-100003', NULL, 100003, '2025-05-04', 'Pending', 'Miscellaneous logistics expenses for food drive.'),
	('INV-100004', 100004, 100004, '2025-05-05', 'Paid', 'Travel reimbursement for senior care volunteers.'),
	('INV-100005', NULL, 100005, '2025-05-06', 'Pending', 'Legal consultation for youth mentorship program.'),
	('INV-100006', 100006, 100006, '2025-05-07', 'Processed', 'Bank processing fees related to garden donations.'),
	('INV-100007', NULL, 100007, '2025-05-08', 'Pending', 'Electricity bills for literacy program facilities.'),
	('INV-100008', 100008, 100008, '2025-05-09', 'Paid', 'Insurance coverage for disaster relief efforts.'),
	('INV-100009', NULL, 100009, '2025-05-10', 'Pending', 'Advertising expenses for propane safety seminar.'),
	('INV-100010', 100010, 100010, '2025-05-11', 'Processed', 'Travel costs for warehouse upgrade team.'),
	('INV-100011', NULL, 100011, '2025-05-12', 'Pending', 'Fundraiser compliance and tax documentation fees.'),
	('INV-100012', 100012, 100012, '2025-05-13', 'Paid', 'Material costs for propane classroom development.'),
	('INV-100013', NULL, 100013, '2025-05-14', 'Pending', 'Projected utility expenses for HVAC system upgrade.'),
	('INV-100014', 100014, 100014, '2025-05-15', 'Processed', 'Transaction processing fees from BBQ event.'),
	('INV-100015', NULL, 100015, '2025-05-16', 'Pending', 'Event coverage expenses for customer appreciation day.'),
	('INV-100016', 100016, 100016, '2025-05-17', 'Paid', 'Marketing campaign costs for propane stockpile effort.'),
	('INV-100017', NULL, 100017, '2025-05-18', 'Pending', 'Miscellaneous expenses for park development planning.')
GO

/*
	<summary>
		Creator:	Kate Rich, Jennifer Nicewanner
		Created:	2025-05-01
		Summary:	Script for inserting sample data into the Donation table.
	</summary>
*/
print '' print '*** Insert Donation Test Records ***'
GO
INSERT INTO [dbo].[Donation]
		([DonationType], [UserID], [ProjectID], [Amount], [DonationDate], [Description])
	VALUES
		('Monetary', 100002, 100002, 100.00, '2024-12-25', 'Propane safety is important!'),
		('Monetary', 100002, 100002, 33.00, '2025-03-03', 'I want to help Uncle Hank more!'),
		('Monetary', 100002, 100004, 250.00, '2025-01-06', 'I wanna help Aunt Peggy with her fundraiser!'),
		('Monetary', 100002, 100006, 302.00, '2025-01-31', 'I want the kids to have clean air to breathe!'),
		('Goods', 100002, 100008, NULL, '2025-02-28', 'I made some lemonade for Uncle Hank''s customer appreciation day.'),
		('Monetary', 100002, 100004, 75.00, '2025-01-08', 'I got paid again so I want to help Aunt Peggy more with her fundraiser!'),
		('Monetary', 100000, 100000, 500.00, '2025-05-01', 'Funds for propane purchase and distribution.'),
		('Goods', 100001, 100001, NULL, '2025-05-02', 'Donated building materials for accessibility upgrades.'),
		('Services', 100002, 100002, NULL, '2025-05-03', 'Volunteer cleanup services for the community park.'),
		('Monetary', 100003, 100003, 750.00, '2025-05-04', 'Financial aid for food drive logistics.'),
		('Goods', 100004, 100004, NULL, '2025-05-05', 'Donated transportation services for senior care volunteers.'),
		('Services', 100005, 100005, NULL, '2025-05-06', 'Mentoring sessions for youth in the program.'),
		('Monetary', 100006, 100006, 1000.00, '2025-05-07', 'Community funds for new gardening equipment.'),
		('Goods', 100007, 100007, NULL, '2025-05-08', 'Books and reading materials for literacy program participants.'),
		('Services', 100008, 100008, NULL, '2025-05-09', 'Volunteer assistance for disaster relief packing.'),
		('Monetary', 100009, 100009, 300.00, '2025-05-10', 'Funding for marketing propane safety seminar.'),
		('Goods', 100010, 100010, NULL, '2025-05-11', 'Shelving materials for warehouse improvement project.'),
		('Services', 100011, 100011, NULL, '2025-05-12', 'Legal and accounting services for fundraiser.'),
		('Monetary', 100012, 100012, 275.00, '2025-05-13', 'Financial support for propane classroom setup.'),
		('Goods', 100013, 100013, NULL, '2025-05-14', 'HVAC equipment parts for system upgrade.'),
		('Services', 100014, 100014, NULL, '2025-05-15', 'Event setup and management for BBQ propane installation.'),
		('Monetary', 100015, 100015, 425.00, '2025-05-16', 'Funds for propane customer appreciation day activities.'),
		('Goods', 100016, 100016, NULL, '2025-05-17', 'Emergency propane tanks for stockpile reserves.'),
		('Services', 100017, 100017, NULL, '2025-05-18', 'Community labor for new park construction.')
GO



/*
	Ellie's
	
	We did not want to delete in case it's needed.
*/
EXEC [dbo].[sp_update_vehicle_active_by_vehicleID] @VehicleID = '1HGCM82633A123456', @Active = 1;


EXEC [dbo].[sp_insert_document]
    @DocumentTypeID = 'Vehicle',                 
    @ReferenceID = '1HGCM82633A123456',                   
    @FileName = 'License.pdf',                
    @FileType = 'Driver License',             
    @Artifact = 0x255044462D312E350D0A25,     
    @Uploader = 100001,                         
    @Description = 'Driver License document for user';