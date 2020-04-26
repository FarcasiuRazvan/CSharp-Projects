USE TheFederationOfAthletes
-- insert into the Competion table
Create Procedure insertRowCompetition
AS
	BEGIN
	declare @NoOFRows int
	declare @n int
	declare @id int
	declare @name VARCHAR(30)
	declare @type VARCHAR(30)
	declare @budget int

	select TOP 1 @NoOFRows=NoOfRows FROM dbo.TestTables TT,dbo.Tables T
	where TT.TableID=T.TableID and T.Name like 'Competition'
	
	SET @n=1
	SET @budget=1000
	WHILE @n<@NoOFRows
	BEGIN
		SET @name='Name'+CONVERT(varchar(5),@n)
		SET @type='Type'+CONVERT(varchar(5),@n)
		SET @id=@n*100
		INSERT INTO Competition(COMID,NAME,TYPE,BUDGET) VALUES(@id,@name,@type,@budget)
		SET @budget=@budget+1000
		SET @n=@n+1
	END
	 
	END
GO

-- insert into the Athlete table
Create Procedure insertRowAthlete
AS
	BEGIN
	declare @NoOFRows int
	declare @n int
	declare @name VARCHAR(30)
	declare @specialization VARCHAR(30)
	declare @id int
	declare @fk int
	declare @fk1 int
	declare @fk2 int

	select TOP 1 @NoOFRows=NoOfRows FROM dbo.TestTables TT,dbo.Tables T
	where TT.TableID=T.TableID and T.Name like 'Athlete'
	
	SET @n=1
	
	WHILE @n<@NoOFRows
	BEGIN
		select top 1 @fk=CID FROM Coach
		select top 1 @fk1=SCID FROM SportingClub
		select top 1 @fk2=NTID FROM NationalTeam
		SET @name='Name'+CONVERT(varchar(5),@n)
		SET @specialization='Specialization'+CONVERT(varchar(5),@n)
		SET @id=@n*100
		INSERT INTO Athlete(AID,NAME,AGE,SPECIALIZATION,CID,SCID,NTID) VALUES(@id,@name,18,@specialization,@fk,@fk1,@fk2)
		SET @n=@n+1
	END
	 
	END
GO

-- insert into the Performs_in table
Create Procedure insertRowPerforms
AS
	BEGIN
	declare @NoOFRows int
	declare @n int
	declare @topRows int
	declare @pk1 int
	declare @pk2 int

	select TOP 1 @NoOFRows=NoOfRows FROM dbo.TestTables TT,dbo.Tables T
	where TT.TableID=T.TableID and T.Name like 'Performs_in'
	
	Select @topRows=count(*) From Athlete a cross join Competition c 

	SET @n=1
	WHILE @n<@NoOFRows and @n<@topRows
	BEGIN

		Select @pk1=ra.AID,@pk2=ra.COMID 
		from(select ROW_NUMBER() OVER(ORDER BY AID ASC) as rowws,AID,COMID from Athlete a cross join Competition c ) as ra
		where ra.rowws=@n
		INSERT INTO Performs_in(AID,COMID) VALUES(@pk1,@pk2)
		SET @n=@n+1
	END
	 
	END
GO

--just a little checking for function ROW_NUMBER
--select *
--from(select ROW_NUMBER() OVER(ORDER BY AID ASC) as rowws,AID,COMID from Athlete a cross join Competition c ) as ra
--where ra.rowws=2

------------------------------------------------------------

-- delete from the Competion table
Create Procedure deleteRowCompetition
AS
	BEGIN
		delete
		from Competition
		where Name like 'Name%'
	END
GO

-- delete from the Athlete table
Create Procedure deleteRowAthlete
AS
	BEGIN
		delete
		from Athlete
		where Name like 'Name%'
	 
	END
GO
-- insert into the Performs_in table
Create Procedure deleteRowPerforms
AS
	BEGIN

	delete
	from Performs_in
	where AID>=100 or COMID>=100

	END
GO

drop procedure insertRows
Create Procedure insertRows
AS
	BEGIN
		--comp,ath,performs_in
		declare @dsCOMP DATETIME
		declare @dfCOMP DATETIME
		declare @dsAth DATETIME
		declare @dfAth DATETIME
		declare @dsPerf DATETIME
		declare @dfPerf DATETIME
		set @dsCOMP= GETDATE()
		exec insertRowCompetition
		set @dfCOMP= GETDATE()

		set @dsAth= GETDATE()
		exec insertRowAthlete
		set @dfAth= GETDATE()

		set @dsPerf= GETDATE()
		exec insertRowPerforms
		set @dfPerf= GETDATE()
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(2,1,@dsCOMP,@dfCOMP)
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(2,2,@dsAth,@dfAth)
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(2,3,@dsPerf,@dfPerf)
	END
GO

drop procedure deleteRows
Create Procedure deleteRows
AS
	BEGIN
		--comp,ath,performs_in
		declare @dsCOMP DATETIME
		declare @dfCOMP DATETIME
		declare @dsAth DATETIME
		declare @dfAth DATETIME
		declare @dsPerf DATETIME
		declare @dfPerf DATETIME
		set @dsPerf= GETDATE()
		exec deleteRowPerforms
		set @dfPerf= GETDATE()

		set @dsAth= GETDATE()
		exec deleteRowAthlete
		set @dfAth= GETDATE()

		set @dsCOMP= GETDATE()
		exec deleteRowCompetition
		set @dfCOMP= GETDATE()
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(1,3,@dsPerf,@dfPerf)
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(1,2,@dsAth,@dfAth)
		INSERT INTO TestRunTables(TestRunID,TableID,StartAt,EndAt)VALUES(1,1,@dsCOMP,@dfCOMP)
	END
GO

Create Procedure dropInsertRows
AS
	BEGIN
		--comp,ath,performs_in
		drop procedure insertRowCompetition
		drop procedure insertRowAthlete
		drop procedure insertRowPerforms
	END
GO

Create Procedure dropDeleteRows
AS
	BEGIN
		--performs_in,ath,comp
		drop procedure deleteRowCompetition
		drop procedure deleteRowAthlete
		drop procedure deleteRowPerforms
	END
GO

exec dropInsertRows
exec dropDeleteRows

select *
from TestRuns

select *
from TestRunTables

select *
from TestRunViews

delete
from TestRunTables

delete
from TestRunViews