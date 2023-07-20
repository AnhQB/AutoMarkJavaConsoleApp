create database project_PRN231
go
use project_PRN231
go
create table Students(
	studentId int primary key identity,
	studentName varchar(120)
)

go
create table Admins(
	adminId int primary key identity,
	username varchar(120),
	password varchar(120)
)


go 
create table Exams(
	examId int ,
	paperNo int,
	examName varchar(120),
	primary key (examId, paperNo)

)

go
create table Questions(
	questionId int primary key identity,
	questionName varchar(50),
	mark float,
	examId int,
	paperNo int,
	foreign key (examId, paperNo) references Exams(examId, paperNo)
)

go
create table TestCases(
	testcaseId int primary key identity,
	input varchar(max),
	output varchar(max),
	mark float,
	questionId int,
	foreign key (questionId) references Questions(questionId)
)

go
create table ExamResults(
	examresultId int primary key identity,
	mark float,
	studentId int,
	examId int,
	paperNo int,
	foreign key (studentId) references Students(studentId),
	foreign key (examId, paperNo) references Exams(examId, paperNo)
)

go 
create table GradeDetail(
	examresultId int,
	questionId int,
	testcaseId int, 
	output varchar(max),
	testresult bit,
	primary key (examresultId, questionId, testcaseId),
	foreign key (examresultId) references ExamResults(examresultId),
	foreign key (questionId) references Questions(questionId),
	foreign key (testcaseId) references TestCases(testcaseId)

)