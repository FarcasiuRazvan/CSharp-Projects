use TheFederationOfAthletes

--a. 2 queries with the union operation; use UNION [ALL] and OR;

--1. What are the names of athletes who belong to AC Viitorul Cluj or whose name starts with C ?
select A.Name,SC.Name
from Athlete A, SportingClub SC
where A.SCID=SC.SCID and SC.Name like 'AC Viitorul Cluj' 
union
select A.Name,SC.Name
from Athlete A, SportingClub SC
where A.SCID=SC.SCID and A.Name like 'C%'

--2. What are the names of the athletes who belong to AC Viitorul Cluj or Racers Track Club or whose name starts with C?
select A.Name,SC.Name
from Athlete A, SportingClub SC
where A.SCID=SC.SCID and (SC.Name like 'AC Viitorul Cluj' or SC.Name like 'Racers Track Club')
union
select A.Name,SC.Name
from Athlete A, SportingClub SC
where A.SCID=SC.SCID and A.Name like 'C%'

--b. 2 queries with the intersection operation; use INTERSECT and IN;

--1. List all the coaches that have age over 40 and belong to a comitee where its budget is 1500.
select CC.CCID ,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and CC.BUDGET=1500
intersect
select CC.CCID,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and C.Age>40

--2. List all the coaches that belong to a comitee where its budget is in the list of budgets of the comitees who have coaches over 40 years old.
select CC.CCID ,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and 
CC.BUDGET IN (select CC.Budget
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and C.Age>40)

--c. 2 queries with the difference operation; use EXCEPT and NOT IN;
--1. List all the coaches that belong to a comitee where its budget is NOT in the list of budgets of the comitees who have coaches over 40 years old.
select CC.CCID ,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and 
CC.BUDGET NOT IN (select CC.Budget
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and C.Age>40)

--2. List all the coaches that belongs to a comitee that has a budget bigger than 300, but the coaches need to be younger than 40.
select CC.CCID ,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and CC.BUDGET>300
except
select CC.CCID,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and C.Age>40

--d. 4 queries with INNER JOIN, LEFT JOIN, RIGHT JOIN, and FULL JOIN; 
--  one query will join at least 3 tables, while another one will join at least two many-to-many relationships;

-- 1. List all the training performed by all the athletes in all stadiums.
select t.NAME AS Tr_Name, a.NAME AS Ath_Name, s.NAME AS Std_Name
from Athlete a inner join Training t
	on a.AID=t.AID
inner join  Stadium s
	on s.SID=t.SID

-- 2. List all the athlets with their trainings and the stadiums on which they are training and if they don't have trainings yet list NULL.
select a.NAME AS Ath_Name,t.NAME AS Tr_Name,s.NAME AS Std_Name
from Athlete a left join Training t
	on a.AID=t.AID
left join  Stadium s
	on s.SID=t.SID

-- 3. What are the names of the stadiums that have no athletes an no trainings ?
select s.NAME AS Std_Name
from Athlete a right join Training t
	on a.AID=t.AID
right join  Stadium s
	on s.SID=t.SID
where a.NAME is NULL and t.NAME is NULL

-- 4. List all the Athletes' name with where they went in cantonment and their trainings (only those who went in cantonment). 

select a.NAME AS Ath_Name,t.NAME AS Tr_Name,s.NAME AS Std_Name,c.Description AS Can_Name
from Athlete a full join Training t
	on a.AID=t.AID
full join  Stadium s
	on s.SID=t.SID
full join Cantonment c
	on c.CANID=t.CANID
where a.NAME is not null and t.NAME is not null and s.NAME is not null and c.Description is not null

--e. 2 queries using the IN operator to introduce a subquery in the WHERE clause; 
--   in at least one query, the subquery should include a subquery in its own WHERE clause;

--1. List all the coaches that belong to a comitee where its budget is in the list of budgets of the comitees who have coaches over 40 years old.
select CC.CCID ,C.CID,C.Name,CC.Name
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and 
CC.BUDGET IN (select CC.Budget
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and C.Age>40)

--2. List all the coaches who belongs to a comitee that has a budget in a list of budgets from the comitees 
--	that have coaches with age in a list that consists of all the ages of coaches that belongs to CC Cluj.  
select *
from Coach C,CoachComitee CC
where C.CCID=CC.CCID and 
CC.BUDGET IN 
(select CC1.Budget
from Coach C1,CoachComitee CC1
where C1.CCID=CC1.CCID and C1.Age in 
	(select C2.Age 
	from Coach C2,CoachComitee CC2
	where C2.CCID=CC2.CCID and CC2.Name='CC Cluj'
	)
)

--f. 2 queries using the EXISTS operator to introduce a subquery in the WHERE clause;
--1. List all the coaches that exists in a list constructed by all the comitees with the name 'CC Cluj' that have coaches, ordered by the name of the coach descending.
select *
from Coach c 
where EXISTS 
(
	SELECT *
	FROM CoachComitee cc
	where cc.NAME like 'CC Cluj' and c.CCID=cc.CCID 
)
order by c.Name desc
--2. List all the athlets that exists in a list constructed by all the trainings with the name '100m sprint' that have athlets
select *
from Athlete a
where EXISTS
(
	select *
	from Training t
	where t.NAME like '100m sprint' and t.AID=a.AID
)

--g. 2 queries with a subquery in the FROM clause;
--1. List all the names from a table that consists of data selected from Training table and the number of repetitions is lower than 20, ordered ascending. 
select x.NAME
from
(	select t.Name,t.NO_OF_REP
	from Training t
	where t.NO_OF_REP<20
) as x
order by x.NAME
--2. List all the names from a table that consists of data selected from Athlete table and the age is bigger than 20. 
select top 1 x.NAME
from
(	select a.NAME,a.AGE
	from Athlete a
	where a.AGE>20
) as x
--h. 4 queries with the GROUP BY clause, 3 of which also contain the HAVING clause; 
--   2 of the latter will also have a subquery in the HAVING clause; use the aggregation operators: COUNT, SUM, AVG, MIN, MAX;

--1. List the age of the youngest person for each specialization.
select *
from Athlete
select top 3 a.SPECIALIZATION, MIN(a.AGE)
from Athlete a
Group By a.SPECIALIZATION

--2. List all the 100m specializations and their number of athletes.
select *
from Athlete
select top 3 a.SPECIALIZATION, Count(*)
from Athlete a
Group By a.SPECIALIZATION
having a.SPECIALIZATION like '%100m'

--3 List top 2 specializations of the athlets, grouped by their name, which have only 1 athlete.
select *
from Athlete

select top 2 a.SPECIALIZATION, Count(*)
from Athlete a
Group By a.SPECIALIZATION
having 1 =
(
	select count(*)
	from Athlete a1
	where a1.SPECIALIZATION = a.SPECIALIZATION
)

--4 List the specialization and the age average of all the coaches grouped by specialization.
select *
from Coach

select c.specialization, avg(c.age)
from Coach c
group by c.specialization
having 1<
(
	select count(*)
	from Coach c1
	where c1.SPECIALIZATION = c.SPECIALIZATION
)

--i. 4 queries using ANY and ALL to introduce a subquery in the WHERE clause; 
--   2 of them should be rewritten with aggregation operators, while the other 2 should also be expressed with [NOT] IN.

--1. List all the athlets who are at least older than the youngest athlete who have the specialization Hurdles100m. 
select *
from Athlete a
where a.AGE > any 
(
	select a1.age
	from Athlete a1
	where a1.SPECIALIZATION='Hurdles100m'
)
--rewrited
select *
from Athlete a
where a.AGE > 
(
	select min(a1.age)
	from Athlete a1
	where a1.SPECIALIZATION='Hurdles100m'
)

--2. List all the coaches who have the same age as one of the coaches who have the specialization Sprint. 
select *
from Coach c 

select *
from Coach c 
where c.AGE = any
(
	select c1.age
	from Coach c1
	where c1.SPECIALIZATION='Sprint'
)
--rewrited

select *
from Coach c 
where c.AGE in
(
	select c1.age
	from Coach c1
	where c1.SPECIALIZATION='Sprint'
)
--3. List all the coaches who are older than all coaches who have the specialization Sprint.
select *
from Coach c 

select *
from Coach c 
where c.AGE > all
(
	select c1.age
	from Coach c1
	where c1.SPECIALIZATION='Sprint'
)
--rewrited


select *
from Coach c 
where c.AGE not in
(
	select c1.age
	from Coach c1
	where c1.SPECIALIZATION='Sprint'
)

--4. List all stadiums which have the capacity bigger than all stadiums which have the name started with Sala de atletism.
select *
from Stadium

select *
from Stadium s
where s.CAPACITTY > all
(
	select s.CAPACITTY
	from Stadium s
	where s.NAME like 'Sala de atletism%'
)
--rewrited
select *
from Stadium s
where s.capacitty >
(
	select max(s.capacitty)
	from Stadium s
	where s.NAME like 'Sala de atletism%'
)


-- DISTINCT
--1
select count(DISTINCT NTID)
from Athlete
select*
from Athlete

--2
select sum(DISTINCT COMID)
from Performs_in
select *
from Performs_in

--3
select avg(DISTINCT Budget)
from Competition
select *
from Competition