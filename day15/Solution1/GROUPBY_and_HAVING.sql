/*
Topic 3 — GROUP BY and HAVING
51.Count students in each department ID, including students with NULL DepartmentId as a group.
52.Find the average age for each department ID.
53.Find the youngest and oldest student age per department.
54.Show department IDs that have more than 10 students.
55.Count students by active status.
56.Count enrollments by EnrollmentStatus.
57.Find courses with more than 15 enrollments.
58.Find the average course fee per category.
59.Count courses per category.
60.Show payment count and total amount by payment status.
61.Show payment count and average amount by payment method.
62.Find payment statuses with at least 20 payments.
63.Find students with more than 3 enrollments.
64.Find students with at least 2 assessment results.
65.Find average marks per assessment.
66.Find assessments whose average marks are above 60.
67.Count result records by SubmissionStatus.
68.Find the total fee per course category for active courses.
69.Find departments with average student age above 22.
70.Count students by DepartmentId and IsActive.
71.Find courses with at least 2 assessments.
72.Find each student's total payment amount.
73.Find students whose total payments exceed 12000.
74.Count results with non-NULL marks per student.
75.Find payment months with more than 10 payments.
*/
--Queries

--51.select departmentid, count(*) as StudentsCount from students group by departmentid;
--52.select departmentid,avg(age) as Average_age  from students group by DepartmentId; 
--53.select departmentid,min(age) as MIN_AGE ,max(age) as MAX_AGE  from students group by DepartmentId; 
--54.select departmentid ,count(studentid) as StudentCount from students group by departmentid having count(studentid)>10;
--55.select count(studentid),isActive from students group by IsActive;
--56.select count(enrollmentid),enrollmentstatus from enrollments group by EnrollmentStatus
--57.select courseid  ,count(enrollmentid) as StudentCount from Enrollments group by courseid having count(EnrollmentId)>15; 
--58.select cast(avg(fee)as decimal(10,2)) as Avg_fee,courseid  from courses group by courseid;
--59.select count(courseid),CourseId as course_count from courses group by courseid;
--60.select count(paymentid) as Payment_count,sum(amount)as Total_amount,PaymentStatus from payments group by PaymentStatus;
--61.select count(paymentid) as Payment_count,avg(amount)as Avg_amount,PaymentMethod from payments group by PaymentMethod;
--62.select count(paymentid) as payment_Count,PaymentStatus from Payments group by PaymentStatus having count(paymentid)>=15;
--63.select studentid ,count(courseid) as EnrolledIN from enrollments group by studentid having count(courseid)>3;
--64.select studentid ,count(resultid) as thier_result_count from AssessmentResults group by studentid having count(resultid)>=2;
--65.select Assessmentid,avg(marksobtained) as Avg_Mark from AssessmentResults group by AssessmentId;
--66.select Assessmentid,avg(marksobtained) as Avg_Mark from AssessmentResults group by AssessmentId having avg(marksobtained)>60;
--67.select count(resultid) as Counts ,submissionstatus from AssessmentResults group by submissionstatus;  
--68.select sum(fee) as Total_fee,courseid from courses where isActive=1 group by courseid;
--69.select avg(age) as Age_avg ,departmentid from students group by DepartmentId having avg(age)>22;
--70.SELECT DepartmentId, IsActive, COUNT(StudentId) AS StudentCount FROM Students GROUP BY DepartmentId, IsActive;
--71.select courseid ,count(assessmentid) as counts from assessments group by courseid having count(assessmentid)>=2;
--72.select sum(amount) as total ,studentid from payments group by studentid;
--73.select sum(amount) as total ,studentid from payments group by studentid having sum(amount)>12000;
--74.SELECT StudentId,COUNT(MarksObtained) AS ResultCount FROM AssessmentResults GROUP BY StudentId;
--75.SELECT YEAR(PaymentDate) AS PaymentYear,MONTH(PaymentDate) AS PaymentMonth COUNT(PaymentId) AS PaymentCount FROM Payments GROUP BY YEAR(PaymentDate), MONTH(PaymentDate) HAVING COUNT(PaymentId) > 10 ORDER BY PaymentYear, PaymentMonth;