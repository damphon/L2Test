# L2Test - Source available at HTTP://github.com/damphon/L2Test
#
#FIXES TO IMPLEMENT FOR BETA BUILD
#Change from 8 answeres to any number of answeres
#Add config page with the following values for leads to change
###How long techs have to enter their UID
###How long techs have to take the test
###Change what percentage constitutes a passing score (change the color shown for grades)
###Text feild leads can use for L2 Test explanation on front page.
###How many questions are on the test.
###Allow different settings for different sites
#####Change what catagorys appear on test (no shuttle questions for non-phoenix sites)
#Stop techs from being able to refresh for a different test.
#
#TODO list for Alpha build
#Add bulk import tool to test edit page
#Make password reset page
#Make install easier and configurabe
##Installer chooses admin login + Password
##Installer chooses DB password
#
#Areas of Focus
#   1 - The testing software should be easy to install and maintain, it should remain usable without me.
#	2 - The Leads need a way to add, remove, and edit questions
#	3 - Employees should be able to verify all answers before final submission
#	4 - The test should be timed, and auto-submit when time runs out.
#	5 - After submitting the results the software should auto grade the test
#	6 - Employees should see the test result as a percentage and a percentage for question category’s (Like the CompTIA tests) This would prevent them from knowing the specific questions they got wrong but aid in future studying. 
#	7 - Leads should be able to review specific answers to all questions
#	8 - Techs can flag questions they think need improvement while taking the test, for leads to review
#	9 - Leads should be able to remove poorly worded questions (from taken tests) and have the test automatically re-score.
#   10 - The results should be saved and easily retrievable at a later date by management for anyone who has taken the test.
#
#Development Notes
#   11/25/2016 - Added new user creation to Manage Users page and am looking into how to reset passwords ect..
#   11/12/2016 - Added text to index page that explains L2 test requierments and instructions for completing the test.
#   11/04/2016 - Spell checked all code while on flight
#   10/29/2016 - When a new tech is added to the login database all entries over 120 days old get purged to keep the database runing smothly. This is ok since that DB only actualy needs to hold the entry for a few hours. 
#   10/29/2016 - Test now auto submits when timer reaches 0 and archive saves the time remaining at the time the test is submitted.
#   10/28/2016 - Added timer to page, timer needs to auto submit test when it reaches 0.
#   10/22/2016 - Fixed grading errors that occured after increasing number of answeres per question.
#   10/15/2016 - Modified database for more stability and to add up to 8 answeres per question, It errors when grading now.
#   10/08/2016 - Created lead's review page for linking to the archived tests and graded tests.
#   10/01/2016 - Test redirects techs to their obfuscated results and graded page is now properly formatted.
#   09/24/2016 - Test now grades with the catagorys.
#   09/10/2016 - Graded results page now indicates which answeres were selected by the tech.
#   09/10/2016 - Test now saves an archive of the test page exactly as it was when the test was submitted prior to grading the test.
#   09/10/2016 - Test now grades correctly even if no answeres are selected. Showed test to Levi, updated TODO list.
#   09/09/2016 - Test now grades correctly but fails if there are no answeres selected.
#   08/28/2016 - Test now grades itself and saves page as html page. The grading and formatting need to be worked on.
#   08/27/2016 - Post goes through successfuly, no longer posting [null] for selected answeres.
#   08/06/2016 - Submit button on test page grades test and saves results as w seperate page that management can look up at any time.
#   07/30/2016 - Test page works with new database structure and can now post selected answeres back to the server.
#   07/29/2016 - Fixed auto fill options on question edit screen.
#   07/28/2016 - Updated Question Edit form to work with new Database structure, needs auto fill options fixed. 
#   07/23/2016 - Remade database to allow for T/F questions, and multiple correct answeres. Also updated add question form to work with the new DB structure.
#   07/22/2016 - Test questions appear on test page, multiple choice only so far. Randomizes the questions and the order answered display in.
#   07/17/2016 - Fixxed issue with techID validation not timing out.
#   07/17/2016 - Added way for leads to create techID's to the manage page.
#   07/17/2016 - Made pages only leads should have access to require authorization.
#   07/16/2016 - Updated index page to request tech ID before taking test.
#   07/16/2016 - Completed webpages that allow leads to add/edit/remove questions on the test.
#   07/09/2016 - Restructured databases and added tech ID DB.
#   07/02/2016 - Got Login page working. Realized that I dont have a way to reset passwords. Also got Questions and ReportCard databases set up.
#   06/18/2016 - Decided not to use any API's as they can change, making this program unusable. That would be contrary to AOF 1. This means I cannot use the API's for jomax login. 
#   06/18/2016 - If I can make it run in Apache 2.2 then it will run with the label printer P3DCAPP/L2Test
