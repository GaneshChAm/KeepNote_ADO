use TestDB
Create Table NotesApp
(
Note_Id int identity primary key,
Note_Title varchar(100),
Note_Description varchar(100),
Note_Date varchar(20)
)

select * from NotesApp
