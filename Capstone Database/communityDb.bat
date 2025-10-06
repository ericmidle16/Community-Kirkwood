echo off

rem batch file to run a sql script with sqlexpress

sqlcmd -S localhost -E -i communityDb.sql

rem Kate Rich DB Additions: Latest - 2025-03-10
sqlcmd -S localhost -E -i KR_Database\USER-011_viewProjectDesktop.sql
sqlcmd -S localhost -E -i KR_Database\BGC-007_createBackgroundCheckDesktop.sql
sqlcmd -S localhost -E -i KR_Database\BGC-008_viewListOfBackgroundChecksDesktop.sql
sqlcmd -S localhost -E -i KR_Database\BGC-010_updateBackgroundCheckDesktop.sql
sqlcmd -S localhost -E -i KR_Database\DON05_viewDonatedProjectsDesktop.sql
sqlcmd -S localhost -E -i KR_Database\BGC-009_viewSingleBackgroundCheck.sql

rem SH_Database additions 2025-02-28
sqlcmd -S localhost -E -i SH_Database\USER_003.sql
sqlcmd -S localhost -E -i SH_Database\VOL_003.sql
sqlcmd -S localhost -E -i SH_Database\PRS_015.sql
sqlcmd -S localhost -E -i SH_Database\VOL_020.sql

rem Syler Bushlack DB Additions
sqlcmd -S localhost -E -i SB_Database/USER-012.sql
sqlcmd -S localhost -E -i SB_Database/PRS-003.sql

rem Brodie Pasker DB Additions
sqlcmd -S localhost -E -i BP_Database\USER-01_login_logout_user.sql
sqlcmd -S localhost -E -i BP_Database\USER-08_update_user.sql
sqlcmd -S localhost -E -i BP_Database\User-014_view_calendar.sql

rem DT_Database additions 2025-03-10
sqlcmd -S localhost -E -i DT_Database\EditListOfNeededItemsPRCH04.sql
sqlcmd -S localhost -E -i DT_Database\removeItemFromNeedListPRS-016.sql
sqlcmd -S localhost -E -i DT_Database\viewNeededItemsPUR-005.sql
sqlcmd -S localhost -E -i DT_Database\viewProfileUSER-007.sql
sqlcmd -S localhost -E -i DT_Database\viewUnassignedVolunteersVLD-015.sql

rem Jacob McPherson DB Additions
sqlcmd -S localhost -E -i JM_Database\ADM07.sql
sqlcmd -S localhost -E -i JM_Database\USER09.sql
sqlcmd -S localhost -E -i JM_Database\VOL09.sql
sqlcmd -S localhost -E -i JM_Database\VOL10.sql
sqlcmd -S localhost -E -i JM_Database\VOL11.sql
sqlcmd -S localhost -E -i JM_Database\VOL14.sql

rem Stan Anderson DB Additions
sqlcmd -S localhost -E -i SA_Database\PRS-010ViewLocations.sql
sqlcmd -S localhost -E -i SA_Database\VLD-005TaskAssignment.sql
sqlcmd -S localhost -E -i SA_Database\VLD-006TaskList.sql
sqlcmd -S localhost -E -i SA_Database\VLD-014ViewEventVolunteers.sql
sqlcmd -S localhost -E -i SA_Database\VOL-013ViewAllExternalContacts.sql
sqlcmd -S localhost -E -i SA_Database\VOL-012ViewSingleExternalContact.sql

rem Jen Nicewanner DB Additions
sqlcmd -S localhost -E -i JN_Database\VLD-017_ViewVolunteerListDesktop.sql
sqlcmd -S localhost -E -i JN_Database\ADM-006_ViewSingleUserDesktop.sql
sqlcmd -S localhost -E -i JN_Database\PRS-012_ViewSingleLocation.sql

rem Nik Bell DB Additions
sqlcmd -S localhost -E -i NB_Database\VOL-024ViewForumPost.sql

rem Josh Nicholson DB Additions
sqlcmd -S localhost -E -i JoN_Database/createProject.sql
sqlcmd -S localhost -E -i JoN_Database/updateSkills.sql
sqlcmd -S localhost -E -i JoN_Database/createTask.sql
sqlcmd -S localhost -E -i JoN_Database/updateTask.sql
sqlcmd -S localhost -E -i JoN_Database/createSkill.sql

rem Yousif Omer DB Additions
sqlcmd -S localhost -E -i YO_Database/EVC-002-UpdateEvent-Desktop.sql
sqlcmd -S localhost -E -i YO_Database/SCH-02-DeleteEvent-Desktop.sql
sqlcmd -S localhost -E -i YO_Database/USER-013-ViewListofEvents-Desktop.sql
sqlcmd -S localhost -E -i YO_Database/USER-018-ViewSingleEvent-Desktop.sql

rem Christivie Mauwa DB Additions
sqlcmd -S localhost -E -i CM_Database\PS05-editProject.sql
sqlcmd -S localhost -E -i CM_Database\DON001-donateToProject.sql
sqlcmd -S localhost -E -i CM_Database\DON007-viewDonationInvoice.sql
sqlcmd -S localhost -E -i CM_Database\DON005-viewDonationHistory.sql

rem Ellie Wacker DB Additions 2025-03-12
sqlcmd -S localhost -E -i EW_Database\VOL-007_leaveProject.sql
sqlcmd -S localhost -E -i EW_Database\DRV-002_addPersonalVehicle.sql
sqlcmd -S localhost -E -i EW_Database\DRV-001_addValidDriversLicense.sql
sqlcmd -S localhost -E -i EW_Database\USER-015_viewListPersonalVehicles.sql
sqlcmd -S localhost -E -i EW_Database\USER_006_resetPassword.sql

sqlcmd -S localhost -E -i SH_Database\VOL_022.sql
sqlcmd -S localhost -E -i SH_Database\VOL_023.sql

rem Eric Idle DB Additions: 2025-04-11
sqlcmd -S localhost -E -i EI_Database\PUR-002.sql
sqlcmd -S localhost -E -i EI_Database\PUR-003.sql
sqlcmd -S localhost -E -i EI_Database\PUR-001.sql
sqlcmd -S localhost -E -i EI_Database\ADMN01.sql
sqlcmd -S localhost -E -i EI_Database\ACC-002.sql
sqlcmd -S localhost -E -i EI_Database\PS03.sql

rem Akoi Kollie DB Additions
sqlcmd -S localhost -E -i AK_Database\ACC-001_changeInvoiceStatus.sql
sqlcmd -S localhost -E -i AK_Database\ACC-004_viewDonationRecord.sql
sqlcmd -S localhost -E -i AK_Database\PRS-007_requestVolunteer.sql
sqlcmd -S localhost -E -i AK_Database\PUR-006_submitInvoice.sql
sqlcmd -S localhost -E -i AK_Database\VOL-006_signUpToProject.sql

rem Brodie Pasker Notification DB Addition
sqlcmd -S localhost -E -i BP_Database\USER-017_view_notifications.sql
sqlcmd -S localhost -E -i BP_Database\SCH-007_schedule_notifications.sql

rem Jackson Manternach DB Additions
sqlcmd -S localhost -E -i JMa_Database\VOL021.sql
sqlcmd -S localhost -E -i JMa_Database\SCH001.sql
sqlcmd -S localhost -E -i JMa_Database\VOL004.sql
sqlcmd -S localhost -E -i JMa_Database\DRV003.sql

rem Chase Hannen DB Additions: Latest 2025-04-04
sqlcmd -S localhost -E -i CH_Database\PRS-009_CreateLocationDesktop.sql
sqlcmd -S localhost -E -i CH_Database\VLD-019_UnassignVolunteersDesktop.sql
sqlcmd -S localhost -E -i CH_Database\VOL-005_ViewAssignedProjectsDesktop.sql
sqlcmd -S localhost -E -i CH_Database\SCH-001_ViewScheduleDesktop.sql
sqlcmd -S localhost -E -i CH_Database\PRS-008_DeactivateLocationDesktop.sql

rem Nik Bell DB Additions 2025-04-11
sqlcmd -S localhost -E -i NB_Database\PRS-011LocationEdit.sql
sqlcmd -S localhost -E -i NB_Database\VOL-028DeleteThreadPosts.sql
sqlcmd -S localhost -E -i NB_Database\VOL-0XXReply.sql

rem Syler Bushlack DB Additions 2025-04-23
sqlcmd -S localhost -E -i SB_Database/MOD-001.sql
sqlcmd -S localhost -E -i SB_Database/MOD-002.sql

rem Kate Rich - New Stored Procedures for Automation Logic, 2025-05-02
sqlcmd -S localhost -E -i storedProcedures.sql

rem Jennifer Nicewanner & Kate Rich - Sample Data, 2025-05-01
sqlcmd -S localhost -E -i sampleData.sql

echo .
echo if no error messages appear, the DB was created (but check) 
pause