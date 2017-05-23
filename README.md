# L2Test - Source available at HTTP://github.com/damphon/L2Test
#
#FIXES TO IMPLEMENT FOR BETA BUILD
#Fix the backups, No more CSV.
#Make the install proccess significantly easier and more stable. 
#Update About page
##############BONUS
#Change what catagorys appear on test (no shuttle questions for non-phoenix sites)
##Make number of or percent of any catagory configurable
#Some kind of check for unanswered questions on submit
#
#Areas of Focus
#   1 - The testing software should be easy to install and maintain, it should remain usable without me.
#	2 - The Leads need a way to add, remove, and edit questions
#	3 - Employees should be able to verify all answers before final submission
#	4 - The test should be timed, and auto-submit when time runs out.
#	5 - After submitting the results the software should auto grade the test
#   10 - The results should be saved and easily retrievable at a later date by management for anyone who has taken the test.
#	6 - Employees should see the test result as a percentage and a percentage for question category’s (Like the CompTIA tests) This would prevent them from knowing the specific questions they got wrong but aid in future studying. 
#	7 - Leads should be able to review specific answers to all questions
#	8 - Techs can flag questions they think need improvement while taking the test, for leads to review
#	9 - Leads should be able to remove poorly worded questions (from taken tests) and have the test automatically re-score.

To use any number of answeres switch to a 2 database formation, and tie the answeres to the questions
ex. 
PID - Question - catagory - (image address)[use a default entrey to indicate no image]
PID - Question's PID - Answer - AnswerID - Correct/incorrect
PID - Question's PID - Answer2 - AnswerID2 - Correct/incorrect

for CSV have the export make a CSV that is 
Question - Catagory - ImageAddress - Answer - Correct/Incorrect - Answer2 - Correct/Incorrect - Answe3 - Correct/Incorrect ect..

For CSV import manualy seperate the information and then put it in as new questions. 



Header
Footer
About Page