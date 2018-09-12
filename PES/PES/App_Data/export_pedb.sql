--------------------------------------------------------
--  File created - Wednesday-January-20-2016   
--------------------------------------------------------
DROP TABLE "PE"."COMMENT" cascade constraints;
DROP TABLE "PE"."DESCRIPTION" cascade constraints;
DROP TABLE "PE"."EMPLOYEE" cascade constraints;
DROP TABLE "PE"."LM_SKILL" cascade constraints;
DROP TABLE "PE"."PE" cascade constraints;
DROP TABLE "PE"."PROFILE" cascade constraints;
DROP TABLE "PE"."SCORE" cascade constraints;
DROP TABLE "PE"."SKILL" cascade constraints;
DROP TABLE "PE"."STATUS" cascade constraints;
DROP TABLE "PE"."SUBTITLE" cascade constraints;
DROP TABLE "PE"."TITLE" cascade constraints;
DROP SYNONYM "PUBLIC"."DUAL";
DROP SYNONYM "PE"."CATALOG";
DROP SYNONYM "PE"."COL";
DROP SYNONYM "PE"."PUBLICSYN";
DROP SYNONYM "PE"."SYSCATALOG";
DROP SYNONYM "PE"."SYSFILES";
DROP SYNONYM "PE"."TAB";
DROP SYNONYM "PE"."TABQUOTAS";
DROP SEQUENCE "PE"."SEQCOMMENT";
DROP SEQUENCE "PE"."SEQDESCRIPTION";
DROP SEQUENCE "PE"."SEQEMPLOYEE";
DROP SEQUENCE "PE"."SEQLMSKILL";
DROP SEQUENCE "PE"."SEQPE";
DROP SEQUENCE "PE"."SEQPROFILE";
DROP SEQUENCE "PE"."SEQSCORE";
DROP SEQUENCE "PE"."SEQSKILL";
DROP SEQUENCE "PE"."SEQSTATUS";
DROP SEQUENCE "PE"."SEQSUBTITLE";
DROP SEQUENCE "PE"."SEQTITLE";
DROP TYPE "PE"."REPCAT$_OBJECT_NULL_VECTOR";
--------------------------------------------------------
--  DDL for Type REPCAT$_OBJECT_NULL_VECTOR
--------------------------------------------------------

  CREATE OR REPLACE TYPE "PE"."REPCAT$_OBJECT_NULL_VECTOR" AS OBJECT
(
  -- type owner, name, hashcode for the type represented by null_vector
  type_owner      VARCHAR2(30),
  type_name       VARCHAR2(30),
  type_hashcode   RAW(17),
  -- null_vector for a particular object instance
  -- ROBJ REVISIT: should only contain the null image, and not version#
  null_vector     RAW(2000)
)

/
--------------------------------------------------------
--  DDL for Sequence SEQCOMMENT
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQCOMMENT"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQDESCRIPTION
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQDESCRIPTION"  MINVALUE 1 MAXVALUE 37 INCREMENT BY 1 START WITH 38 CACHE 20 ORDER  CYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQEMPLOYEE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQEMPLOYEE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 121 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQLMSKILL
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQLMSKILL"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQPE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQPE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQPROFILE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQPROFILE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQSCORE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQSCORE"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQSKILL
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQSKILL"  MINVALUE 1 MAXVALUE 18 INCREMENT BY 1 START WITH 18 CACHE 17 ORDER  CYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQSTATUS
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQSTATUS"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 41 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQSUBTITLE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQSUBTITLE"  MINVALUE 1 MAXVALUE 7 INCREMENT BY 1 START WITH 7 CACHE 6 ORDER  CYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQTITLE
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQTITLE"  MINVALUE 1 MAXVALUE 3 INCREMENT BY 1 START WITH 3 CACHE 2 ORDER  CYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQLATENESS
--------------------------------------------------------

   CREATE SEQUENCE "PE"."SEQLATENESS" MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20; 
--------------------------------------------------------
--  DDL for Table COMMENT
--------------------------------------------------------

   CREATE TABLE "PE"."LATENESS"
    ( "ID_LATENESS" NUMERIC NOT NULL,
    "DATE" TIMESTAMP NOT NULL,
    "ID_EMPLOYEE" NUMERIC NOT NULL,
	"DELETE_STATUS" NUMERIC(1) DEFAULT '0' NOT NULL,
    CONSTRAINT "LATENESS_ID_LATENESS_PK" PRIMARY KEY("ID_LATENESS")
    )SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "SYSTEM" ;
    
--------------------------------------------------------
--  DDL for Table LOCATION
--------------------------------------------------------

    CREATE TABLE "PE"."LOCATION" 
    ( "ID_LOCATION" NUMERIC(3) NOT NULL,
    "NAME" VARCHAR2(25) NOT NULL,
    CONSTRAINT "LOCATION_ID_LOCATION_PK" PRIMARY KEY("ID_LOCATION")
    )SEGMENT CREATION IMMEDIATE 
    PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
    STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
    PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
    TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table COMMENT
--------------------------------------------------------

  CREATE TABLE "PE"."COMMENT" 
   (	"ID_COMMENT" NUMBER, 
	"ID_PE" NUMBER, 
	"TRAINNING_EMPLOYEE" VARCHAR2(500 CHAR), 
	"TRAINNING_EVALUATOR" VARCHAR2(500 CHAR), 
	"ACKNOWLEDGE_EVALUATOR" VARCHAR2(500 CHAR), 
	"comm/recomm_employee" VARCHAR2(500 CHAR), 
	"comm/recomm_evaluator" VARCHAR2(500 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table DESCRIPTION
--------------------------------------------------------

  CREATE TABLE "PE"."DESCRIPTION" 
   (	"ID_DESCRIPTION" NUMBER, 
	"DESCRIPTION" VARCHAR2(200 CHAR), 
	"ID_SUBTITLE" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table EMPLOYEE
--------------------------------------------------------

  CREATE TABLE "PE"."EMPLOYEE" 
   (	"ID_EMPLOYEE" NUMBER, 
	"FIRST_NAME" VARCHAR2(50 CHAR), 
	"LAST_NAME" VARCHAR2(50 CHAR),
	"NICK_NAME" VARCHAR(100 CHAR), 
	"EMAIL" VARCHAR2(50 CHAR), 
	"CUSTOMER" VARCHAR2(100 CHAR), 
	"POSITION" VARCHAR2(100 CHAR), 
	"ID_PROFILE" NUMBER, 
	"ID_MANAGER" NUMBER, 
	"HIRE_DATE" DATE, 
	"END_DATE" DATE, 
	"PROJECT" VARCHAR2(100 BYTE),
  "ID_LOCATION" NUMERIC(3) NOT NULL
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table LM_SKILL
--------------------------------------------------------

  CREATE TABLE "PE"."LM_SKILL" 
   (	"ID_LMSKILL" NUMBER, 
	"ID_SKILL" NUMBER, 
	"ID_PE" NUMBER, 
	"CHECK_EMPLOYEE" CHAR(1 BYTE), 
	"CHECK_EVALUATOR" CHAR(1 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PE  FIELDS ADDED TO COMPLETE THE PE TABLE ON WENDESDAY 02-11-2016 BY IVAN MESINA
--------------------------------------------------------

  CREATE TABLE "PE"."PE" 
   (	"ID_PE" NUMBER, 
	"EVALUATION_PERIOD" DATE, 
	"ID_EMPLOYEE" NUMBER, 
	"ID_EVALUATOR" NUMBER, 
	"ID_STATUS" NUMBER, 
	"TOTAL" NUMBER, 
	"ENGLISH_SCORE" NUMBER, 
	"PERFORMANCE_SCORE" NUMBER, 
	"COMPETENCE_SCORE" NUMBER, 
	"RANK" NUMBER,
	"EVALUATION_YEAR" NUMBER NOT NULL, 
	"ID_PERIOD" NUMBER NOT NULL
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;

--------------------------------------------------------
--  DDL for Table PERIOD  CREATION OF PERIOD TABLE TO ALLOW ENTER TO "UPLOAD PERFORMANCE FILE" ON THUESDAY 03-11-2016 BY IVAN MESINA
--------------------------------------------------------

   CREATE TABLE "PE"."PERIOD"
   (	"ID_PERIOD" NUMBER,
	"NAME" VARCHAR2(30 CHAR),
	CONSTRAINT PER_ID_PERIOD_PK PRIMARY KEY ("ID_PERIOD")
	)
	PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
	STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
	PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
	TABLESPACE "SYSTEM" ;

--------------------------------------------------------
--  DDL for Table PROFILE
--------------------------------------------------------

  CREATE TABLE "PE"."PROFILE" 
   (	"ID_PROFILE" NUMBER, 
	"PROFILE" VARCHAR2(10 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SCORE
--------------------------------------------------------

  CREATE TABLE "PE"."SCORE" 
   (	"ID_SCORE" NUMBER, 
	"ID_DESCRIPTION" NUMBER, 
	"ID_PE" NUMBER, 
	"SCORE_EMPLOYEE" NUMBER, 
	"SCORE_EVALUATOR" NUMBER, 
	"COMMENTS" VARCHAR2(150 CHAR), 
	"CALCULATION" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SKILL
--------------------------------------------------------

  CREATE TABLE "PE"."SKILL" 
   (	"ID_SKILL" NUMBER, 
	"SKILL" VARCHAR2(100 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table STATUS
--------------------------------------------------------

  CREATE TABLE "PE"."STATUS" 
   (	"ID_STATUS" NUMBER, 
	"STATUS" VARCHAR2(20 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SUBTITLE
--------------------------------------------------------

  CREATE TABLE "PE"."SUBTITLE" 
   (	"ID_SUBTITLE" NUMBER, 
	"SUBTITLE" VARCHAR2(130 CHAR), 
	"ID_TITLE" NUMBER
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table TITLE
--------------------------------------------------------

  CREATE TABLE "PE"."TITLE" 
   (	"ID_TITLE" NUMBER, 
	"TITLE" VARCHAR2(30 CHAR)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
REM INSERTING into PE."COMMENT"
SET DEFINE OFF;
REM INSERTING into PE.DESCRIPTION
SET DEFINE OFF;
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (1,'1. Accuracy or Precision',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (2,'2. Thoroughness (Content) and Neatness (Presentation)',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (3,'3. Reliability',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (4,'4. Responsiveness to requests for service',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (5,'5. Follow-through/Follow-up',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (6,'6. Judgment/Decision making',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (7,'Subtotal',1);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (8,'7.Priority Setting',2);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (9,'8.Amount of work completed',2);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (10,'9.Work completed on schedule',2);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (11,'Subtotal',2);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (12,'Total Performance',2);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (13,'10. Job Knowledge',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (14,'11. Analyzes Problems',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (15,'12. Flexible / Adaptable',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (16,'13. Planning and Organization',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (17,'14. Competent/proper usage of work tools',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (18,'15. Follows proper procedures, standards and requirements',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (19,'Subtotal',3);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (20,'16. With Supervisors',4);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (21,'17. With other team members / across teams',4);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (22,'18. With client(s)',4);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (23,'19. Commitment to Team Success',4);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (24,'Subtotal',4);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (25,'20. Actively seeks ways to streamline processes',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (26,'21. Open to new ideas and approaches',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (27,'22. Involvement/commitment in activities for work/company improvement',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (28,'23. Challenges Status Quo processes in appropriate ways',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (29,'24. Seeks additional training and development',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (30,'Subtotal',5);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (31,'Punctuality: Fulfillment of the company''s established schedules for attendance, meetings, etc (within the company and with clients). ',6);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (32,'Policies Compliance: (non-disclosure, dress code )',6);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (33,'Values: Acts according to the company values ',6);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (34,'Subtotal',6);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (35,'ENGLISH EVALUATION result',6);
Insert into PE.DESCRIPTION (ID_DESCRIPTION,DESCRIPTION,ID_SUBTITLE) values (36,'Total  Competences',6);
REM INSERTING into PE.EMPLOYEE
SET DEFINE OFF;
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (1,'Jose Francisco','Adan','adan,josefrancisco','jose.adan@4thsource.com','No Customer','.NET Developer',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (2,'Eduardo','Aguilar','aguilar,eduardo','jose.aguilar@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (3,'Lao-kiun','Alcantar Lam','alcantarlam,lao-kiun','felipe.alcantar@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (4,'Israel','Alcantar','alcantar,israel','israel.alcantar@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (5,'Diego','Amaya','amaya,diego','Diego.amaya@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (6,'Monica','Benavides','benavides,monica','monica.benavides@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (7,'Javier','Cernas','cernas,javier','javier.cernas@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (8,'Jose','Cortes','cortes,jose','jose.cortes@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (9,'Jonathan','Covarrubias','covarrubias,jonathan','jonathan.covarrubias@4thsource.com','No Customer','Quality Assurance Analyst',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (10,'Patrick','De la Rosa','delarosa,patrick','patrick.delarosa@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (11,'Victor','Escalera','escalera,victor','victor.escalera@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (12,'Carlos','Esparza','esparza,carlos','Carlos.esparza@4thsource.com','No Customer','Quality Assurance Analyst',2,26,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (13,'Manuel','Fragoso','fragoso,manuel','manuel.fragoso@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (14,'Gustavo','Fuentes','fuentes,gustavo','gustavo.fuentes@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (15,'Laura','Garcia Paez','garcia,laura','laura.garcia@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (16,'Oscar','Garcia','garcia,oscar','Mauricio.garcia@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (17,'Jorge','Gomez','gomez,jorge','jorge.gomez@4thsource.com','No Customer','Developer',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (18,'Hector','Gonzalez','gonzales,hector','hector.gonzales@4thsource.com','No Customer','Developer',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (19,'Jose','Gutierrez','gutierrez,jose','jose.gutierrez@4thsource.com','No Customer','Developer',2,26,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (20,'Alan','Guzman','guzman,alan','alan.guzman@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (21,'Carlos','Hernandez','hernandez,carlos','Carlos.hernandez@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (22,'Miguel','Hernandez','hernandez,miguel','miguel.hernandez@4thsource.com','No Customer','Developer',3,26,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (23,'Victor','Hernandez','hernandez,victor','victor.hernandez@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (24,'Saul','Jacinto','jacinto,saul','saul.casiano@4thsource.com','No Customer','Developer',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (25,'Jose','Lazcano','lazcano,jose','jose.lazcano@4thsource.com','No Customer','RM',2,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (26,'Victor Angel','Leon Gonzalez','leon,victor','victor.leon@4thsource.com','No Customer','.NET Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (27,'Francisco','Lomeli','lomeli,francisco','francisco.lomeli@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (28,'Guillermo','Lopez','lopez,guillermo','guillermo.lopez@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (29,'Daniel','Lopez','lopez,daniel','daniel.lopez@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (30,'Jamie','Luna','luna,jaime','jaime.luna@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (31,'Jose','Martinez','martinez,josemaria','jose.martinez@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (32,'Eric','Mesina','mesina,eric','eric.mesina@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (33,'Ivan','Mesina','mesina,ivan','ivan.mesina@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (34,'Nestor','Morales','morales,nestor','nestor.julian@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (35,'Eduardo','Morin','morin,eduardo','eduardo.morin@4thsource.com','No Customer','Developer',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (36,'Anibal','Negrete','negrete,anibal','anibal.negrete@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (37,'Mario','Nunez','nunez,mario','mario.nunez@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (38,'Eder','Palacios','palacios,eder','eder.palacios@4thsource.com','No Customer','Developer',2,26,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (39,'Orlando','Palacios','palacios,orlando','orlando.palacios@4thsource.com','No Customer','Quality Assurance Analyst',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (40,'Victor','Pena','pena,victor','victor.pena@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (41,'Elena','Peralta','peralta,elena','elena.peralta@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (42,'Daniel','Perez','perez,daniel','daniel.perez@4thsource.com','No Customer','Quality Assurance Analyst',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (43,'Roberto','Quintero','quintero,roberto','roberto.quintero@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (44,'Fernando','Reyes','reyes,fernando','fernando.reyes@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (45,'Juan','Ricardo','ricardo,juan','juan.ricardo@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (46,'Ghersain','Rivera','rivera,ghersain','azael.rivera@4thsource.com','No Customer','Quality Assurance Analyst',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (47,'Martha','Rodriguez','rodriguez,martha','martha.rodriguez@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (48,'Santiago','Rodriguez','rodriguez,santiago','santiago.rodriguez@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (49,'Eduardo','Ruiz','ruiz,eduardo','eduardo.ruiz@4thsource.com','No Customer','Developer',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (50,'Christian','Ruiz','ruiz,christian','christian.ruiz@4thsource.com','No Customer','Quality Assurance Analyst',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (51,'Jose','Salazar','salazar,jose','jose.salazar@4thsource.com','No Customer','Developer',2,26,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (52,'Carlos','Sandoval','sandoval,carlos','carlos.sandoval@4thsource.com','No Customer','Quality Assurance Analyst',1,39,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (53,'Juan','Sepulveda','sepulveda,juan','juan.sepulveda@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (54,'Alejandro','Servin','servin,alejandro','alejandro.servin@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (55,'Felipe','Torres','torres,felipe','felipe.torres@4thsource.com','No Customer','Developer',1,49,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (56,'Jose','Vaca','vaca,eduardo','jose.vaca@4thsource.com','No Customer','Developer',1,23,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (57,'Fernando','Vasquez','vasquez,fernando','fernando.vasquez@4thsource.com','No Customer','Developer',1,15,to_date('12-22-2015','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (58,'Jazmin','Vazquez','vazquez,jazmin','jazmin.vazquez@4thsource.com','No Customer','Developer',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,NICK_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION) values (59,'Jose','Zepeda','zepeda,jose','jose.zepeda@4thsource.com','No Customer','Quality Assurance Analyst',1,59,to_date('01-01-1900','MM-DD-YYYY'),null,null,2);

REM INSERTING into PE.LM_SKILL
SET DEFINE OFF;
REM INSERTING into PE.PE
SET DEFINE OFF;
REM INSERTING into PE.PROFILE
SET DEFINE OFF;
Insert into PE.PROFILE (ID_PROFILE,PROFILE) values (1,'Resource');
Insert into PE.PROFILE (ID_PROFILE,PROFILE) values (2,'Manager');
Insert into PE.PROFILE (ID_PROFILE,PROFILE) values (3,'Director');
REM INSERTING into PE.SCORE
SET DEFINE OFF;
REM INSERTING into PE.SKILL
SET DEFINE OFF;
Insert into PE.SKILL (ID_SKILL,SKILL) values (2,'Coordinates activities with the client or is the main contact');
Insert into PE.SKILL (ID_SKILL,SKILL) values (3,'Defines the tech approach and/ or project plan');
Insert into PE.SKILL (ID_SKILL,SKILL) values (4,'Supports and observes company policies');
Insert into PE.SKILL (ID_SKILL,SKILL) values (5,'Keeps control and follows up for the plan');
Insert into PE.SKILL (ID_SKILL,SKILL) values (6,'Generates business opportunities');
Insert into PE.SKILL (ID_SKILL,SKILL) values (7,'Trains and develops team members');
Insert into PE.SKILL (ID_SKILL,SKILL) values (8,'Supports experimentation and brainstorming that leads to innovation and learning');
Insert into PE.SKILL (ID_SKILL,SKILL) values (9,'Evaluates team regularly');
Insert into PE.SKILL (ID_SKILL,SKILL) values (10,'Faces performance problems in an honest, straightforward manner');
Insert into PE.SKILL (ID_SKILL,SKILL) values (11,'Supports responsible risk taking');
Insert into PE.SKILL (ID_SKILL,SKILL) values (1,'Supervises personnel');
Insert into PE.SKILL (ID_SKILL,SKILL) values (12,'Helps control costs and maximize resources');
Insert into PE.SKILL (ID_SKILL,SKILL) values (13,'Instills pride, service, innovation and quality');
Insert into PE.SKILL (ID_SKILL,SKILL) values (14,'Sets high standards for self, as well as others');
Insert into PE.SKILL (ID_SKILL,SKILL) values (15,'Supports useful debate and disagreement');
Insert into PE.SKILL (ID_SKILL,SKILL) values (16,'Welcomes constructive criticism');
Insert into PE.SKILL (ID_SKILL,SKILL) values (17,'Sets specific goals for simplicity, productivity and process improvements');
REM INSERTING into PE.STATUS
SET DEFINE OFF;
Insert into PE.STATUS (ID_STATUS,STATUS) values (1,'Incomplete');
Insert into PE.STATUS (ID_STATUS,STATUS) values (3,'Complete Evaluator');
Insert into PE.STATUS (ID_STATUS,STATUS) values (2,'Complete Employee');
REM INSERTING into PE.SUBTITLE
SET DEFINE OFF;
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (3,'Skills and Knowledge',2);
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (4,'INTERPERSONAL SKILLS: Effectiveness of the team member''s interaction with others and as a team participant',2);
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (5,'Growth and development: Learns new concepts and techniques, investigates and explores new work processes and/or new tools.',2);
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (6,'Policies Compliance: (non-disclosure, dress code )',2);
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (1,'Quality of the Developed Products: The products meet all the requirements, specifications and standards that the client requires?',1);
Insert into PE.SUBTITLE (ID_SUBTITLE,SUBTITLE,ID_TITLE) values (2,'Opportunity in the delivery of products: All products were delivered on or before deadlines?',1);
REM INSERTING into PE.TITLE
SET DEFINE OFF;
Insert into PE.TITLE (ID_TITLE,TITLE) values (1,'Performance');
Insert into PE.TITLE (ID_TITLE,TITLE) values (2,'Competences');
REM INSERTING into PE.LOCATION
SET DEFINE OFF;
INSERT INTO PE.LOCATION (ID_LOCATION, NAME) VALUES (1,'USA');
INSERT INTO PE.LOCATION (ID_LOCATION, NAME) VALUES (2,'COLIMA');
INSERT INTO PE.LOCATION (ID_LOCATION, NAME) VALUES (3,'MERIDA');
INSERT INTO PE.LOCATION (ID_LOCATION, NAME) VALUES (4,'DF');
--------------------------------------------------------
--  DDL for Index COMMENT_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."COMMENT_PK" ON "PE"."COMMENT" ("ID_COMMENT") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index DESCRIPTION_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."DESCRIPTION_PK" ON "PE"."DESCRIPTION" ("ID_DESCRIPTION") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index EMPLOYEE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."EMPLOYEE_PK" ON "PE"."EMPLOYEE" ("ID_EMPLOYEE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index LM_SKILL_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."LM_SKILL_PK" ON "PE"."LM_SKILL" ("ID_LMSKILL") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."PE_PK" ON "PE"."PE" ("ID_PE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index PROFILE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."PROFILE_PK" ON "PE"."PROFILE" ("ID_PROFILE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SCORE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."SCORE_PK" ON "PE"."SCORE" ("ID_SCORE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SKILL_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."SKILL_PK" ON "PE"."SKILL" ("ID_SKILL") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index STATUS_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."STATUS_PK" ON "PE"."STATUS" ("ID_STATUS") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index SUBTITLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."SUBTITLE_PK" ON "PE"."SUBTITLE" ("ID_SUBTITLE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Index TITLE_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "PE"."TITLE_PK" ON "PE"."TITLE" ("ID_TITLE") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  Constraints for Table COMMENT
--------------------------------------------------------

  ALTER TABLE "PE"."COMMENT" ADD CONSTRAINT "COMMENT_PK" PRIMARY KEY ("ID_COMMENT")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."COMMENT" MODIFY ("ID_PE" NOT NULL ENABLE);
  ALTER TABLE "PE"."COMMENT" MODIFY ("ID_COMMENT" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table DESCRIPTION
--------------------------------------------------------

  ALTER TABLE "PE"."DESCRIPTION" ADD CONSTRAINT "DESCRIPTION_PK" PRIMARY KEY ("ID_DESCRIPTION")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."DESCRIPTION" MODIFY ("ID_SUBTITLE" NOT NULL ENABLE);
  ALTER TABLE "PE"."DESCRIPTION" MODIFY ("DESCRIPTION" NOT NULL ENABLE);
  ALTER TABLE "PE"."DESCRIPTION" MODIFY ("ID_DESCRIPTION" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table EMPLOYEE
--------------------------------------------------------

  ALTER TABLE "PE"."EMPLOYEE" ADD CONSTRAINT "EMPLOYEE_PK" PRIMARY KEY ("ID_EMPLOYEE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("HIRE_DATE" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("ID_MANAGER" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("ID_PROFILE" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("POSITION" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("CUSTOMER" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("EMAIL" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("LAST_NAME" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("FIRST_NAME" NOT NULL ENABLE);
  ALTER TABLE "PE"."EMPLOYEE" MODIFY ("ID_EMPLOYEE" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table LM_SKILL
--------------------------------------------------------

  ALTER TABLE "PE"."LM_SKILL" ADD CONSTRAINT "LM_SKILL_PK" PRIMARY KEY ("ID_LMSKILL")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."LM_SKILL" MODIFY ("ID_PE" NOT NULL ENABLE);
  ALTER TABLE "PE"."LM_SKILL" MODIFY ("ID_SKILL" NOT NULL ENABLE);
  ALTER TABLE "PE"."LM_SKILL" MODIFY ("ID_LMSKILL" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table PE
--------------------------------------------------------

  ALTER TABLE "PE"."PE" MODIFY ("COMPETENCE_SCORE" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("PERFORMANCE_SCORE" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" ADD CONSTRAINT "PE_PK" PRIMARY KEY ("ID_PE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."PE" MODIFY ("TOTAL" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("ID_STATUS" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("ID_EVALUATOR" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("ID_EMPLOYEE" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("EVALUATION_PERIOD" NOT NULL ENABLE);
  ALTER TABLE "PE"."PE" MODIFY ("ID_PE" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table PROFILE
--------------------------------------------------------

  ALTER TABLE "PE"."PROFILE" ADD CONSTRAINT "PROFILE_PK" PRIMARY KEY ("ID_PROFILE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."PROFILE" MODIFY ("PROFILE" NOT NULL ENABLE);
  ALTER TABLE "PE"."PROFILE" MODIFY ("ID_PROFILE" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SCORE
--------------------------------------------------------

  ALTER TABLE "PE"."SCORE" ADD CONSTRAINT "SCORE_PK" PRIMARY KEY ("ID_SCORE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."SCORE" MODIFY ("CALCULATION" NOT NULL ENABLE);
  ALTER TABLE "PE"."SCORE" MODIFY ("SCORE_EVALUATOR" NOT NULL ENABLE);
  ALTER TABLE "PE"."SCORE" MODIFY ("SCORE_EMPLOYEE" NOT NULL ENABLE);
  ALTER TABLE "PE"."SCORE" MODIFY ("ID_PE" NOT NULL ENABLE);
  ALTER TABLE "PE"."SCORE" MODIFY ("ID_DESCRIPTION" NOT NULL ENABLE);
  ALTER TABLE "PE"."SCORE" MODIFY ("ID_SCORE" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SKILL
--------------------------------------------------------

  ALTER TABLE "PE"."SKILL" MODIFY ("SKILL" NOT NULL ENABLE);
  ALTER TABLE "PE"."SKILL" MODIFY ("ID_SKILL" NOT NULL ENABLE);
  ALTER TABLE "PE"."SKILL" ADD CONSTRAINT "SKILL_PK" PRIMARY KEY ("ID_SKILL")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
--------------------------------------------------------
--  Constraints for Table STATUS
--------------------------------------------------------

  ALTER TABLE "PE"."STATUS" ADD CONSTRAINT "STATUS_PK" PRIMARY KEY ("ID_STATUS")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."STATUS" MODIFY ("STATUS" NOT NULL ENABLE);
  ALTER TABLE "PE"."STATUS" MODIFY ("ID_STATUS" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table SUBTITLE
--------------------------------------------------------

  ALTER TABLE "PE"."SUBTITLE" ADD CONSTRAINT "SUBTITLE_PK" PRIMARY KEY ("ID_SUBTITLE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."SUBTITLE" MODIFY ("ID_TITLE" NOT NULL ENABLE);
  ALTER TABLE "PE"."SUBTITLE" MODIFY ("SUBTITLE" NOT NULL ENABLE);
  ALTER TABLE "PE"."SUBTITLE" MODIFY ("ID_SUBTITLE" NOT NULL ENABLE);
--------------------------------------------------------
--  Constraints for Table TITLE
--------------------------------------------------------

  ALTER TABLE "PE"."TITLE" ADD CONSTRAINT "TITLE_PK" PRIMARY KEY ("ID_TITLE")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE;
  ALTER TABLE "PE"."TITLE" MODIFY ("TITLE" NOT NULL ENABLE);
  ALTER TABLE "PE"."TITLE" MODIFY ("ID_TITLE" NOT NULL ENABLE);
--------------------------------------------------------
--  Ref Constraints for Table LATENESS
--------------------------------------------------------

  ALTER TABLE "PE"."LATENESS" ADD CONSTRAINT "LATENESS_ID_EMPLOYEE_FK" FOREIGN KEY("ID_EMPLOYEE") 
    REFERENCES "PE"."EMPLOYEE"("ID_EMPLOYEE") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table COMMENT
--------------------------------------------------------

  ALTER TABLE "PE"."COMMENT" ADD CONSTRAINT "COMMENT_PE_FK" FOREIGN KEY ("ID_PE")
	  REFERENCES "PE"."PE" ("ID_PE") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DESCRIPTION
--------------------------------------------------------

  ALTER TABLE "PE"."DESCRIPTION" ADD CONSTRAINT "DESCRIPTION_SUBTITLE_FK" FOREIGN KEY ("ID_SUBTITLE")
	  REFERENCES "PE"."SUBTITLE" ("ID_SUBTITLE") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table EMPLOYEE
--------------------------------------------------------

  ALTER TABLE "PE"."EMPLOYEE" ADD CONSTRAINT "EMPLOYEE_PROFILE_FK" FOREIGN KEY ("ID_PROFILE")
	  REFERENCES "PE"."PROFILE" ("ID_PROFILE") ENABLE;
  ALTER TABLE "PE"."EMPLOYEE" ADD CONSTRAINT "EMPLOYEE_ID_LOCATION_FK" FOREIGN KEY("ID_LOCATION") 
    REFERENCES "PE"."LOCATION"("ID_LOCATION") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table LM_SKILL
--------------------------------------------------------

  ALTER TABLE "PE"."LM_SKILL" ADD CONSTRAINT "LM_SKILL_PE_FK" FOREIGN KEY ("ID_PE")
	  REFERENCES "PE"."PE" ("ID_PE") ENABLE;
  ALTER TABLE "PE"."LM_SKILL" ADD CONSTRAINT "LM_SKILL_SKILL_FK" FOREIGN KEY ("ID_SKILL")
	  REFERENCES "PE"."SKILL" ("ID_SKILL") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PE
--------------------------------------------------------

  ALTER TABLE "PE"."PE" ADD CONSTRAINT "PE_EMPLOYEE_EVALUATOR_FK" FOREIGN KEY ("ID_EVALUATOR")
	  REFERENCES "PE"."EMPLOYEE" ("ID_EMPLOYEE") DISABLE;
  ALTER TABLE "PE"."PE" ADD CONSTRAINT "PE_EMPLOYEE_FK" FOREIGN KEY ("ID_EMPLOYEE")
	  REFERENCES "PE"."EMPLOYEE" ("ID_EMPLOYEE") DISABLE;
  ALTER TABLE "PE"."PE" ADD CONSTRAINT "PE_STATUS_FK" FOREIGN KEY ("ID_STATUS")
	  REFERENCES "PE"."STATUS" ("ID_STATUS") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SCORE
--------------------------------------------------------

  ALTER TABLE "PE"."SCORE" ADD CONSTRAINT "SCORE_DESCRIPTION_FK" FOREIGN KEY ("ID_DESCRIPTION")
	  REFERENCES "PE"."DESCRIPTION" ("ID_DESCRIPTION") ENABLE;
  ALTER TABLE "PE"."SCORE" ADD CONSTRAINT "SCORE_PE_FK" FOREIGN KEY ("ID_PE")
	  REFERENCES "PE"."PE" ("ID_PE") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table SUBTITLE
--------------------------------------------------------

  ALTER TABLE "PE"."SUBTITLE" ADD CONSTRAINT "SUBTITLE_TITLE_FK" FOREIGN KEY ("ID_TITLE")
	  REFERENCES "PE"."TITLE" ("ID_TITLE") ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGLATENESS
--------------------------------------------------------
  CREATE OR REPLACE TRIGGER "PE"."TRIGGLATENESS" 
  BEFORE INSERT ON "PE"."LATENESS" 
  FOR EACH ROW
  BEGIN
    if inserting then 
      if :NEW."ID_LATENESS" is null then
        SELECT SEQLATENESS.NEXTVAL INTO :NEW."ID_LATENESS" FROM dual;
      end if; 
    end if; 
  END;
  /
--------------------------------------------------------
--  DDL for Trigger TRIGGCOMMENT
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGCOMMENT" 
   before insert on "PE"."COMMENT" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_COMMENT" is null then 
         select SEQCOMMENT.nextval into :NEW."ID_COMMENT" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "PE"."TRIGGCOMMENT" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGDESCRIPTION
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGDESCRIPTION" 
   before insert on "PE"."DESCRIPTION" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_DESCRIPTION" is null then 
         select SEQDESCRIPTION.nextval into :NEW."ID_DESCRIPTION" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "PE"."TRIGGDESCRIPTION" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGEMPLOYEE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGEMPLOYEE" 
   before insert on "PE"."EMPLOYEE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_EMPLOYEE" is null then 
         select SEQEMPLOYEE.nextval into :NEW."ID_EMPLOYEE" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGEMPLOYEE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGLMSKILL
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGLMSKILL" 
   before insert on "PE"."LM_SKILL" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_LMSKILL" is null then 
         select SEQLMSKILL.nextval into :NEW."ID_LMSKILL" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "PE"."TRIGGLMSKILL" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGPE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGPE" 
   before insert on "PE"."PE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_PE" is null then 
         select SEQPE.nextval into :NEW."ID_PE" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "PE"."TRIGGPE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGPROFILE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGPROFILE" 
   before insert on "PE"."PROFILE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_PROFILE" is null then 
         select SEQPROFILE.nextval into :NEW."ID_PROFILE" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGPROFILE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGSCORE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGSCORE" 
   before insert on "PE"."SCORE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_SCORE" is null then 
         select SEQSCORE.nextval into :NEW."ID_SCORE" from dual; 
      end if; 
   end if; 
end;

/
ALTER TRIGGER "PE"."TRIGGSCORE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGSKILL
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGSKILL" 
   before insert on "PE"."SKILL" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_SKILL" is null then 
         select SEQSKILL.nextval into :NEW."ID_SKILL" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGSKILL" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGSTATUS
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGSTATUS" 
   before insert on "PE"."STATUS" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_STATUS" is null then 
         select SEQSTATUS.nextval into :NEW."ID_STATUS" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGSTATUS" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGSUBTITLE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGSUBTITLE" 
   before insert on "PE"."SUBTITLE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_SUBTITLE" is null then 
         select SEQSUBTITLE.nextval into :NEW."ID_SUBTITLE" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGSUBTITLE" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGTITLE
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGTITLE" 
   before insert on "PE"."TITLE" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_TITLE" is null then 
         select SEQTITLE.nextval into :NEW."ID_TITLE" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGTITLE" ENABLE;
--------------------------------------------------------
--  DDL for Synonymn DUAL
--------------------------------------------------------

  CREATE OR REPLACE PUBLIC SYNONYM "DUAL" FOR "SYS"."DUAL";
--------------------------------------------------------
--  DDL for Synonymn CATALOG
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."CATALOG" FOR "SYS"."CATALOG";
--------------------------------------------------------
--  DDL for Synonymn COL
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."COL" FOR "SYS"."COL";
--------------------------------------------------------
--  DDL for Synonymn PUBLICSYN
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."PUBLICSYN" FOR "SYS"."PUBLICSYN";
--------------------------------------------------------
--  DDL for Synonymn SYSCATALOG
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."SYSCATALOG" FOR "SYS"."SYSCATALOG";
--------------------------------------------------------
--  DDL for Synonymn SYSFILES
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."SYSFILES" FOR "SYS"."SYSFILES";
--------------------------------------------------------
--  DDL for Synonymn TAB
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."TAB" FOR "SYS"."TAB";
--------------------------------------------------------
--  DDL for Synonymn TABQUOTAS
--------------------------------------------------------

  CREATE OR REPLACE SYNONYM "PE"."TABQUOTAS" FOR "SYS"."TABQUOTAS";
  