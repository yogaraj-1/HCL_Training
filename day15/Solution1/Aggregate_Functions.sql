/*Topic 2 — Aggregate Functions
26.Count all students.
27.Count students who have an email value.
28.Count students who have a department assigned.
29.Find the youngest student age.
30.Find the oldest student age.
31.Calculate the average student age with two decimal places.
32.Find the total number of credits across all courses.
33.Find the average course fee.
34.Find the lowest and highest course fees.
35.Count distinct student ages.
36.Count distinct non-NULL department IDs represented in Students.
37.Count all enrollment records.
38.Count completed enrollments.
39.Calculate the total amount of all payments.
40.Calculate the total amount of payments with status Paid.
41.Find the average marks obtained across non-NULL assessment results.
42.Find the highest mark obtained.
43.Find the lowest non-NULL mark obtained.
44.Count assessment results with no marks.
45.Count submitted results.
46.Calculate the average marks for submitted results only.
47.Find the total number of assessments.
48.Find the total maximum marks across all assessments.
49.Find the most recent payment date.
50.Find the total number of active courses.

*/
--Queries
/*USE LearnerManagementAssessment;
Go*/

--26.select count(*) as No_of_Students from Students;
--27.Select count(*) from students where email is not null;
--28.Select count(*) from students where DepartmentId is not null;
--29.Select min(age) from students;
--30.Select max(age) from students;
--31.Select CAST(AVG(Age * 1.0) AS DECIMAL(10,2)) from students
--32.select sum(credits) as total_credits from Courses;
--33.select cast(avg(fee) as decimal(10,2)) as Avg_Fee from Courses;
--34.select max(fee) as MAX_FEE ,min(fee) as MIN_FEE from courses;
--35.SELECT COUNT(DISTINCT Age) AS AgeCount FROM Students;
--36.SELECT COUNT(DISTINCT DepartmentId) AS DistinctDepartmentCount FROM Students;
--37.select count(enrollmentid) as total_Enrollments from enrollments;
--38.select count(*) as total_completed from enrollments where EnrollmentStatus='completed';
--39.select count(*) as total_payments from payments;
--40.select count(*) as total_paid_payments from payments where paymentstatus='paid';
--41.SELECT cast(AVG(MarksObtained) as decimal(10,2)) AS AverageMarks FROM AssessmentResults;
--42.SELECT max(MarksObtained)  AS MaxMark FROM AssessmentResults;
--43.SELECT min(MarksObtained)  AS MaxMark FROM AssessmentResults;
--44.select count(*) as NO_marks from AssessmentResults where MarksObtained is  null;
--45.select count(*)  from AssessmentResults where SubmissionStatus='submitted';
--46.select avg(MarksObtained) from AssessmentResults where SubmissionStatus='submitted'
--47.select count(*) from assessments;
--48.select Sum(maxmarks) from assessments;
--49.select max(paymentDate) as recent_date from payments;
--50.select count(*) as Active_courses from Courses where isActive=1;