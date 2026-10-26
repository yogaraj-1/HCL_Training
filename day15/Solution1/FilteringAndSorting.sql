/*Topic 1 — Filtering and Sorting
1.Display the StudentId, Name, Email, and Age of all students.
2.Find students who are exactly 21 years old.
3.Find students younger than 20.
4.Find active students aged 20 or above.
5.Find students aged between 18 and 25 inclusive.
6.Find students belonging to departments 1, 3, or 6.
7.Find students who have not been assigned a department.
8.Find students who have been assigned a department.
9.Find names that begin with A.
10.Find names that end with the digit 5.
11.Find names containing the text 'ar' (use case-insensitive matching where supported by the database collation).
12.Find names whose second character is 'a'.
13.Find names that contain exactly one character before ' ' and then any text.
14.Find emails ending in example.com.
15.Find students whose age is not 20.
16.Find students aged 18–22 who are active.
17.Find students who are inactive or are not assigned to a department.
18.Sort students from youngest to oldest.
19.Display the 10 oldest students.
20.Sort students by DepartmentId ascending, then Age descending.
21.Display each distinct age value.
22.Display distinct non-NULL department IDs.
23.Find names containing a digit using a LIKE pattern.
24.Find names containing any uppercase letter from A through M at the beginning.
25.Find students admitted during calendar year 2025.
*/


--1.select StudentId, Name, Email, Age ,departmentid from Students;
--2.select StudentId,Name,Age from students where age=21;
--3.select  StudentId,Name,Age from students where age<20;
--4.select  StudentId,Name from students where age>=20 AND isActive=1;
--5.select  StudentId,Name,Age from students where age>=18 AND age<=25;
--6.select  StudentId,Name,departmentid from students where departmentid=1 OR departmentid=3 OR departmentid=6;
--7.select  StudentId,Name,departmentid from students where departmentid Is NULL;
--8.select  StudentId,Name,departmentid from students where departmentid IS NOT NULL;
--9.select  StudentId,Name from students where name LIKE 'A%';
--10.select  StudentId,Name from students where NAME LIKE '%5';
--11.select  StudentId,Name from students where NAME LIKE '%ar%';
--12.select  StudentId,Name from students where Name LIKE '_a%';
--13.select  StudentId,Name from students where Name LIKE '_ %';
--14.select  StudentId,Name,email from students where email like '%example.com';
--15.select  StudentId,Name,Age from students where age!=20;
--16.select  StudentId,Name,Age,isActive from students where (age>=18 AND age<=22) AND isActive=1 ;
--17.select  StudentId,Name,isActive,departmentid from students where IsActive=0 OR DepartmentId IS NULL;
--18.select  StudentId,Name,Age from students Order BY age ;
--19.select TOP 10 StudentId, Name, Age, Email from  Students ORDER BY Age DESC;
--20.select * from students order by DepartmentId Asc,age Desc;
--21.select age from students group by age ;
--22.select departmentid from students where departmentid is Not null group by departmentid; 
--23.SELECT StudentId, Name FROM Students WHERE Name LIKE '%[0-9]%';
--24.SELECT StudentId, Name FROM Students WHERE Name COLLATE Latin1_General_100_BIN2 LIKE '[A-M]%';
--25.SELECT StudentId, Name, Email, AdmissionDate FROM Students WHERE AdmissionDate >= '20250101' AND AdmissionDate < '20260101' ORDER BY AdmissionDate ASC;