# L2Test - Source available at HTTP://github.com/damphon/L2Test
#
#TODO
#
#Give leads ability to add leads or techs (techs get a temp password that expires) (Techs dont log in they get a one time code) Make another DB table.
#Make password reset page
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
#   07/16/2016 - Updated index page to request tech ID before taking test.
#   07/16/2016 - Completed webpages that allow leads to add/edit/remove questions on the test.
#   07/09/2016 - Restructured databases and added tech ID DB.
#   07/02/2016 - Got Login page working. Realized that I dont have a way to reset passwords. Also got Questions and ReportCard databases set up.
#   06/18/2016 - Decided not to use any API's as they can change, making this program unusable. That would be contrary to AOF 1. This means I cannot use the API's for jomax login. 
#   06/18/2016 - If I can make it run in Apache 2.2 then it will run with the label printer P3DCAPP/L2Test
