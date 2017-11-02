--------------------------------------------------------
-- Archivo creado  - jueves-diciembre-01-2016   and Update - Jueves-Octubre-10-2017
--------------------------------------------------------
DROP TABLE "PE"."COMMENT" cascade constraints;
DROP TABLE "PE"."DESCRIPTION" cascade constraints;
DROP TABLE "PE"."EMPLOYEE" cascade constraints;
DROP TABLE "PE"."HOLIDAY_DAYS" cascade constraints;
DROP TABLE "PE"."LATENESS" cascade constraints;
DROP TABLE "PE"."LM_SKILL" cascade constraints;
DROP TABLE "PE"."LOCATION" cascade constraints;
DROP TABLE "PE"."PE" cascade constraints;
DROP TABLE "PE"."PERIOD" cascade constraints;
DROP TABLE "PE"."PROFILE" cascade constraints;
DROP TABLE "PE"."SCORE" cascade constraints;
DROP TABLE "PE"."SKILL" cascade constraints;
DROP TABLE "PE"."STATUS" cascade constraints;
DROP TABLE "PE"."SUBTITLE" cascade constraints;
DROP TABLE "PE"."TITLE" cascade constraints;
DROP TABLE "PE"."VACATION_HEADER_REQ" cascade constraints;
DROP TABLE "PE"."VACATION_REQ_STATUS" cascade constraints;
DROP TABLE "PE"."VACATION_SUBREQ" cascade constraints;


DROP SEQUENCE "PE"."SEQCOMMENT";
DROP SEQUENCE "PE"."SEQDESCRIPTION";
DROP SEQUENCE "PE"."SEQEMPLOYEE";
DROP SEQUENCE "PE"."SEQHOLIDAY_DAYS";
DROP SEQUENCE "PE"."SEQLATENESS";
DROP SEQUENCE "PE"."SEQLMSKILL";
DROP SEQUENCE "PE"."SEQPE";
DROP SEQUENCE "PE"."SEQPROFILE";
DROP SEQUENCE "PE"."SEQSCORE";
DROP SEQUENCE "PE"."SEQSKILL";
DROP SEQUENCE "PE"."SEQSTATUS";
DROP SEQUENCE "PE"."SEQSUBTITLE";
DROP SEQUENCE "PE"."SEQTITLE";
DROP SEQUENCE "PE"."SEQVAC_HEAD_REQ";
DROP SEQUENCE "PE"."SEQVAC_REQ_STATUS";
DROP SEQUENCE "PE"."SEQVAC_SUBREQ";


DROP SYNONYM "PE"."CATALOG";
DROP SYNONYM "PE"."COL";
DROP SYNONYM "PE"."PUBLICSYN";
DROP SYNONYM "PE"."SYSCATALOG";
DROP SYNONYM "PE"."SYSFILES";
DROP SYNONYM "PE"."TAB";
DROP SYNONYM "PE"."TABQUOTAS";
DROP SYNONYM "PUBLIC"."DUAL";

  
--------------------------------------------------------
--  DDL for Table COMMENT
--------------------------------------------------------

  CREATE TABLE "PE"."COMMENT" 
   (	"ID_COMMENT" NUMBER NOT NULL, 
	"ID_PE" NUMBER, 
	"TRAINNING_EMPLOYEE" VARCHAR2(500 CHAR), 
	"TRAINNING_EVALUATOR" VARCHAR2(500 CHAR), 
	"ACKNOWLEDGE_EVALUATOR" VARCHAR2(500 CHAR), 
	"comm/recomm_employee" VARCHAR2(500 CHAR), 
	"comm/recomm_evaluator" VARCHAR2(500 CHAR),
    PRIMARY KEY ("ID_COMMENT")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
  
--------------------------------------------------------
--  DDL for Table DESCRIPTION
--------------------------------------------------------

  CREATE TABLE "PE"."DESCRIPTION" 
   (	"ID_DESCRIPTION" NUMBER NOT NULL, 
	"DESCRIPTION" VARCHAR2(200 CHAR), 
	"ID_SUBTITLE" NUMBER,
    PRIMARY KEY ("ID_DESCRIPTION")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;  
--------------------------------------------------------
--  DDL for Table EMPLOYEE
--------------------------------------------------------

  CREATE TABLE "PE"."EMPLOYEE" 
   (	"ID_EMPLOYEE" NUMBER NOT NULL , 
	"FIRST_NAME" VARCHAR2(50 CHAR), 
	"LAST_NAME" VARCHAR2(50 CHAR), 
	"EMAIL" VARCHAR2(50 CHAR), 
	"CUSTOMER" VARCHAR2(100 CHAR), 
	"POSITION" VARCHAR2(100 CHAR), 
	"ID_PROFILE" NUMBER, 
	"ID_MANAGER" NUMBER, 
	"HIRE_DATE" DATE, 
	"END_DATE" DATE, 
	"PROJECT" VARCHAR2(100 BYTE), 
	"ID_LOCATION" NUMBER(3,0), 
	"FREE_DAYS" NUMBER(3,0) DEFAULT 0,
     PRIMARY KEY ("ID_EMPLOYEE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table HOLIDAY_DAYS
--------------------------------------------------------

  CREATE TABLE "PE"."HOLIDAY_DAYS" 
   (	"ID_HOLIDAY" NUMBER(4,0) NOT NULL , 
	"HOLIDAY_DAY" DATE,
    PRIMARY KEY ("ID_HOLIDAY")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table LATENESS
--------------------------------------------------------

  CREATE TABLE "PE"."LATENESS" 
   (	"ID_LATENESS" NUMBER(*,0) NOT NULL, 
	"DATE" TIMESTAMP (6), 
	"ID_EMPLOYEE" NUMBER(*,0),
     PRIMARY KEY ("ID_LATENESS")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table LM_SKILL
--------------------------------------------------------

  CREATE TABLE "PE"."LM_SKILL" 
   (	"ID_LMSKILL" NUMBER NOT NULL, 
	"ID_SKILL" NUMBER, 
	"ID_PE" NUMBER, 
	"CHECK_EMPLOYEE" CHAR(1 BYTE), 
	"CHECK_EVALUATOR" CHAR(1 BYTE),
     PRIMARY KEY ("ID_LMSKILL")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table LOCATION
--------------------------------------------------------

  CREATE TABLE "PE"."LOCATION" 
   (	"ID_LOCATION" NUMBER(3,0) NOT NULL, 
	"NAME" VARCHAR2(25 BYTE),
    PRIMARY KEY ("ID_LOCATION")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PE
--------------------------------------------------------

  CREATE TABLE "PE"."PE" 
   (	"ID_PE" NUMBER NOT NULL, 
	"EVALUATION_PERIOD" DATE, 
	"ID_EMPLOYEE" NUMBER, 
	"ID_EVALUATOR" NUMBER, 
	"ID_STATUS" NUMBER, 
	"TOTAL" NUMBER, 
	"ENGLISH_SCORE" NUMBER, 
	"PERFORMANCE_SCORE" NUMBER, 
	"COMPETENCE_SCORE" NUMBER, 
	"RANK" NUMBER, 
	"EVALUATION_YEAR" NUMBER, 
	"ID_PERIOD" NUMBER,
    PRIMARY KEY ("ID_PE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PERIOD
--------------------------------------------------------

  CREATE TABLE "PE"."PERIOD" 
   (	"ID_PERIOD" NUMBER NOT NULL, 
	"NAME" VARCHAR2(30 CHAR),
    PRIMARY KEY ("ID_PERIOD")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table PROFILE
--------------------------------------------------------

  CREATE TABLE "PE"."PROFILE" 
   (	"ID_PROFILE" NUMBER NOT NULL, 
	"PROFILE" VARCHAR2(10 CHAR),
     PRIMARY KEY ("ID_PROFILE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SCORE
--------------------------------------------------------

  CREATE TABLE "PE"."SCORE" 
   (	"ID_SCORE" NUMBER NOT NULL, 
	"ID_DESCRIPTION" NUMBER, 
	"ID_PE" NUMBER, 
	"SCORE_EMPLOYEE" NUMBER, 
	"SCORE_EVALUATOR" NUMBER, 
	"COMMENTS" VARCHAR2(150 CHAR), 
	"CALCULATION" NUMBER,
    PRIMARY KEY ("ID_SCORE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SKILL
--------------------------------------------------------

  CREATE TABLE "PE"."SKILL" 
   (	"ID_SKILL" NUMBER NOT NULL, 
	"SKILL" VARCHAR2(100 CHAR),
    PRIMARY KEY ("ID_SKILL")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table STATUS
--------------------------------------------------------

  CREATE TABLE "PE"."STATUS" 
   (	"ID_STATUS" NUMBER NOT NULL, 
	"STATUS" VARCHAR2(20 CHAR),
    PRIMARY KEY ("ID_STATUS")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table SUBTITLE
--------------------------------------------------------

  CREATE TABLE "PE"."SUBTITLE" 
   (	"ID_SUBTITLE" NUMBER NOT NULL, 
	"SUBTITLE" VARCHAR2(130 CHAR), 
	"ID_TITLE" NUMBER,
     PRIMARY KEY ("ID_SUBTITLE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table TITLE
--------------------------------------------------------

  CREATE TABLE "PE"."TITLE" 
   (	"ID_TITLE" NUMBER NOT NULL, 
	"TITLE" VARCHAR2(30 CHAR),
    PRIMARY KEY ("ID_TITLE")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table VACATION_HEADER_REQ
--------------------------------------------------------

  CREATE TABLE "PE"."VACATION_HEADER_REQ" 
   (	"ID_HEADER_REQ" NUMBER(4,0) NOT NULL , 
	"ID_EMPLOYEE" NUMBER(4,0), 
	"TITLE" VARCHAR2(30 BYTE), 
	"NO_VAC_DAYS" NUMBER(2,0), 
	"COMMENTS" VARCHAR2(300 BYTE), 
	"ID_REQ_STATUS" NUMBER(2,0), 
	"REPLAY_COMMENT" VARCHAR2(300 BYTE), 
	"NO_UNPAID_DAYS" NUMBER(2,0),
     PRIMARY KEY ("ID_HEADER_REQ")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table VACATION_REQ_STATUS
--------------------------------------------------------

  CREATE TABLE "PE"."VACATION_REQ_STATUS" 
   (	"ID_REQ_STATUS" NUMBER(2,0) NOT NULL, 
	"REQ_STATUS" VARCHAR2(10 BYTE),
     PRIMARY KEY ("ID_REQ_STATUS")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
--------------------------------------------------------
--  DDL for Table VACATION_SUBREQ
--------------------------------------------------------

  CREATE TABLE "PE"."VACATION_SUBREQ" 
   (	"ID_SUBREQ" NUMBER(4,0) NOT NULL, 
	"ID_HEADER_REQ" NUMBER(4,0), 
	"START_DATE" DATE, 
	"END_DATE" DATE, 
	"RETURN_DATE" DATE, 
	"HAVE_PROJECT" VARCHAR2(1 BYTE), 
	"LEAD_NAME" VARCHAR2(30 BYTE),
    PRIMARY KEY ("ID_SUBREQ")
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;

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
--  DDL for Sequence SEQHOLIDAY_DAYS
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQHOLIDAY_DAYS"  MINVALUE 1 MAXVALUE 99999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQLATENESS
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQLATENESS"  MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
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
--  DDL for Sequence SEQVAC_HEAD_REQ
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQVAC_HEAD_REQ"  MINVALUE 1 MAXVALUE 999999999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQVAC_REQ_STATUS
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQVAC_REQ_STATUS"  MINVALUE 1 MAXVALUE 999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 ORDER  NOCYCLE ;
--------------------------------------------------------
--  DDL for Sequence SEQVAC_SUBREQ
--------------------------------------------------------

   CREATE SEQUENCE  "PE"."SEQVAC_SUBREQ"  MINVALUE 1 MAXVALUE 99999999999999999999999 INCREMENT BY 1 START WITH 21 CACHE 20 ORDER  NOCYCLE ;
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
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (1,'Jose','Adan','jose.adan@4thsource.com','No Customer','.NET Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (2,'Jose Eduardo','Aguilar Anguiano','eduardo.aguilar@4thsource.com','No Customer','Developer',2,2,to_date('22/12/15','DD/MM/RR'),null,null,2,4);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (3,'Israel','Alcantar','israel.alcantar@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (4,'Alan','Altamirano','alan.altamirano@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (5,'Diego','Amaya','Diego.amaya@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (6,'Jhonathan','Amezcua','jhonathan.amezcua@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (7,'Monica','Benavides','monica.benavides@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (8,'Christian','Borja','christian.borja@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (9,'Saul','Casiano','saul.casiano@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (10,'Ricardo','Castaneda','ricardo.castaneda@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (11,'Javier Alexis','Cernas Avalos','javier.cernas@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (12,'Jose Eduardo','Cortes Cernas','jose.cortes@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (13,'Jonathan','Covarrubias','jonathan.covarrubias@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (14,'Patrick','De la Rosa','patrick.delarosa@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (15,'Carlos','Esparza','Carlos.esparza@4thsource.com','No Customer','Quality Assurance Analyst',2,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (16,'Idalia','Evangelista','idalia.evangelista@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (18,'Gustavo','Fuentes','gustavo.fuentes@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (20,'Laura','Garcia Paez','laura.garcia@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (19,'Oscar Mauricio','Garcia','Mauricio.garcia@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (21,'Yessica','Godinez','jesica.godinez@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (22,'Oscar','Gonzalez','oscar.gonzalez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (23,'Jose','Gutierrez','jose.gutierrez@4thsource.com','No Customer','Developer',2,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (24,'Alan','Guzman','alan.guzman@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (25,'Carlos','Hernandez','Carlos.hernandez@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (26,'Miguel','Hernandez','miguel.hernandez@4thsource.com','No Customer','Developer',3,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (27,'Victor','Hernandez','victor.hernandez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (28,'Victor Angel','Le�n Gonz�lez','victor.leon@4thsource.com','No Customer','.NET Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (29,'Noe','Leos','noe.leos@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (30,'Francisco','Lomeli','francisco.lomeli@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (31,'Daniel','Lopez','daniel.lopez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (32,'Jamie','Luna Garcia','jaime.luna@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (33,'Alejandra','Mayorga','alejandra.mayorga@4thsource.com','No Customer','Project Manager',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (34,'Eric','Mesina','eric.mesina@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (35,'Nestor','Morales','nestor.julian@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (36,'Eduardo','Morin','eduardo.morin@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (37,'Luis','Munguia Gonzalez','luis.munguia@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (38,'Anibal','Negrete','anibal.negrete@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (39,'Eder','Palacios','eder.palacios@4thsource.com','No Customer','Developer',2,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (40,'Orlando','Palacios','orlando.palacios@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (41,'Victor','Pena','victor.pena@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (42,'Daniel','Perez','daniel.perez@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (43,'Robertho','Perez','robertho.perez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (44,'Juan','Ricardo','juan.ricardo@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (45,'Ghersain','Rivera','azael.rivera@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (46,'Martha','Rodriguez','martha.rodriguez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (47,'Santiago','Rodriguez','santiago.rodriguez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (48,'Christian','Ruiz','christian.ruiz@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (49,'Jose','Salazar','jose.salazar@4thsource.com','No Customer','Developer',2,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (50,'Carlos','Sandoval','carlos.sandoval@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (51,'Juan Ruben','Sepulveda','juan.sepulveda@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (52,'Felipe','Torres','felipe.torres@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (53,'Jose','Vaca','jose.vaca@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (54,'Bruno','Vargas Camarena','bruno.vargas@4thsource.com','No Customer','Quality Assurance Analyst',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (55,'Fernando','Vasquez','fernando.vasquez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (56,'Josimar','Vazquez Plaza','josimar.vazquez@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (58,'Alejandro','Munoz Osorio','alejandro.munoz@4thsource.com','No Customer','Developer',1,2,to_date('23/10/17','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (59,'Jesus','Gutierrez','jesus.gutierrez@4thsource.com','No Customer','Developer',1,2,to_date('23/10/17','DD/MM/RR'),null,null,2,0);
Insert into PE.EMPLOYEE (ID_EMPLOYEE,FIRST_NAME,LAST_NAME,EMAIL,CUSTOMER,POSITION,ID_PROFILE,ID_MANAGER,HIRE_DATE,END_DATE,PROJECT,ID_LOCATION,FREE_DAYS) values (57,'Julio','Virgen','julio.virgen@4thsource.com','No Customer','Developer',1,2,to_date('22/12/15','DD/MM/RR'),null,null,2,0);
REM INSERTING into PE.HOLIDAY_DAYS
SET DEFINE OFF;
REM INSERTING into PE.LATENESS
SET DEFINE OFF;
REM INSERTING into PE.LM_SKILL
SET DEFINE OFF;
REM INSERTING into PE.LOCATION
SET DEFINE OFF;
Insert into PE.LOCATION (ID_LOCATION,NAME) values (1,'USA');
Insert into PE.LOCATION (ID_LOCATION,NAME) values (2,'COLIMA');
Insert into PE.LOCATION (ID_LOCATION,NAME) values (3,'MERIDA');
Insert into PE.LOCATION (ID_LOCATION,NAME) values (4,'DF');
REM INSERTING into PE.PE
SET DEFINE OFF;
REM INSERTING into PE.PERIOD
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
REM INSERTING into PE.VACATION_HEADER_REQ
SET DEFINE OFF;
Insert into PE.VACATION_HEADER_REQ (ID_HEADER_REQ,ID_EMPLOYEE,TITLE,NO_VAC_DAYS,COMMENTS,ID_REQ_STATUS,REPLAY_COMMENT,NO_UNPAID_DAYS) values (1,2,'Christmas vacations',4,'My family are going to come and i would like to spend time with them.',1,null,null);
Insert into PE.VACATION_HEADER_REQ (ID_HEADER_REQ,ID_EMPLOYEE,TITLE,NO_VAC_DAYS,COMMENTS,ID_REQ_STATUS,REPLAY_COMMENT,NO_UNPAID_DAYS) values (2,2,'I need this vacations please',3,'My family are going to come and i would like to spend time with them.',3,null,null);
Insert into PE.VACATION_HEADER_REQ (ID_HEADER_REQ,ID_EMPLOYEE,TITLE,NO_VAC_DAYS,COMMENTS,ID_REQ_STATUS,REPLAY_COMMENT,NO_UNPAID_DAYS) values (3,12,'GOOD FRIDAY',2,'DSFBHJSBF JHSB F',1,null,null);
Insert into PE.VACATION_HEADER_REQ (ID_HEADER_REQ,ID_EMPLOYEE,TITLE,NO_VAC_DAYS,COMMENTS,ID_REQ_STATUS,REPLAY_COMMENT,NO_UNPAID_DAYS) values (5,2,'Necessary days',2,'DSFBHJSBF JHSB F',2,'You have tasks for those days',null);
Insert into PE.VACATION_HEADER_REQ (ID_HEADER_REQ,ID_EMPLOYEE,TITLE,NO_VAC_DAYS,COMMENTS,ID_REQ_STATUS,REPLAY_COMMENT,NO_UNPAID_DAYS) values (8,2,'adfsdf',3,'ddasfasdf',1,null,null);
REM INSERTING into PE.VACATION_REQ_STATUS
SET DEFINE OFF;
Insert into PE.VACATION_REQ_STATUS (ID_REQ_STATUS,REQ_STATUS) values (1,'PENDING');
Insert into PE.VACATION_REQ_STATUS (ID_REQ_STATUS,REQ_STATUS) values (2,'REJECTED');
Insert into PE.VACATION_REQ_STATUS (ID_REQ_STATUS,REQ_STATUS) values (3,'APPROVED');
Insert into PE.VACATION_REQ_STATUS (ID_REQ_STATUS,REQ_STATUS) values (4,'CANCELED');
REM INSERTING into PE.VACATION_SUBREQ
SET DEFINE OFF;
Insert into PE.VACATION_SUBREQ (ID_SUBREQ,ID_HEADER_REQ,START_DATE,END_DATE,RETURN_DATE,HAVE_PROJECT,LEAD_NAME) values (1,1,to_date('24/11/16','DD/MM/RR'),to_date('30/11/16','DD/MM/RR'),to_date('01/12/16','DD/MM/RR'),null,null);
Insert into PE.VACATION_SUBREQ (ID_SUBREQ,ID_HEADER_REQ,START_DATE,END_DATE,RETURN_DATE,HAVE_PROJECT,LEAD_NAME) values (2,2,to_date('25/11/16','DD/MM/RR'),to_date('29/11/16','DD/MM/RR'),to_date('30/12/16','DD/MM/RR'),null,null);
Insert into PE.VACATION_SUBREQ (ID_SUBREQ,ID_HEADER_REQ,START_DATE,END_DATE,RETURN_DATE,HAVE_PROJECT,LEAD_NAME) values (3,3,to_date('26/11/16','DD/MM/RR'),to_date('28/11/16','DD/MM/RR'),to_date('29/11/16','DD/MM/RR'),null,null);
Insert into PE.VACATION_SUBREQ (ID_SUBREQ,ID_HEADER_REQ,START_DATE,END_DATE,RETURN_DATE,HAVE_PROJECT,LEAD_NAME) values (4,5,to_date('26/11/16','DD/MM/RR'),to_date('28/11/16','DD/MM/RR'),to_date('29/11/16','DD/MM/RR'),null,null);
    

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
--  DDL for Trigger TRIGGHOLIDAY_DAYS
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGHOLIDAY_DAYS" 
   before insert on "PE"."HOLIDAY_DAYS" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_HOLIDAY" is null then 
         select SEQHOLIDAY_DAYS.nextval into :NEW."ID_HOLIDAY" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGHOLIDAY_DAYS" ENABLE;
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
ALTER TRIGGER "PE"."TRIGGLATENESS" ENABLE;
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
--  DDL for Trigger TRIGGVAC_HEADER_REQ
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGVAC_HEADER_REQ" 
   BEFORE INSERT ON "PE"."VACATION_HEADER_REQ"
   for each row 
BEGIN
   if inserting then 
      if :NEW."ID_HEADER_REQ" is null then 
         select SEQVAC_HEAD_REQ.nextval into :NEW."ID_HEADER_REQ" from dual; 
      end if; 
   end if; 
END;
/
ALTER TRIGGER "PE"."TRIGGVAC_HEADER_REQ" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGVAC_REQ_STATUS
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGVAC_REQ_STATUS" 
   before insert on "PE"."VACATION_REQ_STATUS" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_REQ_STATUS" is null then 
         select SEQVAC_REQ_STATUS.nextval into :NEW."ID_REQ_STATUS" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGVAC_REQ_STATUS" ENABLE;
--------------------------------------------------------
--  DDL for Trigger TRIGGVAC_SUBREQ
--------------------------------------------------------

  CREATE OR REPLACE TRIGGER "PE"."TRIGGVAC_SUBREQ" 
   before insert on "PE"."VACATION_SUBREQ" 
   for each row 
begin  
   if inserting then 
      if :NEW."ID_SUBREQ" is null then 
         select SEQVAC_SUBREQ.nextval into :NEW."ID_SUBREQ" from dual; 
      end if; 
   end if; 
end;
/
ALTER TRIGGER "PE"."TRIGGVAC_SUBREQ" ENABLE;
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
--------------------------------------------------------
--  DDL for Synonymn DUAL
--------------------------------------------------------

  CREATE OR REPLACE PUBLIC SYNONYM "DUAL" FOR "SYS"."DUAL";    
    
-- FOREIGN KEYS 

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
  ALTER TABLE "PE"."PE" ADD CONSTRAINT "PE_ID_PERIOD_PK" FOREIGN KEY ("ID_PERIOD")
	  REFERENCES "PE"."PERIOD" ("ID_PERIOD") ENABLE;
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
--  Ref Constraints for Table VACATION_HEADER_REQ
--------------------------------------------------------

  ALTER TABLE "PE"."VACATION_HEADER_REQ" ADD CONSTRAINT "VAC_HEADER_REQ_ID_EMPLOYEE_FK" FOREIGN KEY ("ID_EMPLOYEE")
	  REFERENCES "PE"."EMPLOYEE" ("ID_EMPLOYEE") ENABLE;  
      
  ALTER TABLE "PE"."VACATION_HEADER_REQ" ADD CONSTRAINT "VH_REQ_ID_SUB_REQ_STATUS_FK" FOREIGN KEY ("ID_REQ_STATUS")
	  REFERENCES "PE"."VACATION_REQ_STATUS" ("ID_REQ_STATUS") ENABLE;

      
--------------------------------------------------------
--  Ref Constraints for Table VACATION_SUBREQ
--------------------------------------------------------     

  ALTER TABLE "PE"."VACATION_SUBREQ" ADD CONSTRAINT "VAC_SUBREQ_ID_HEADER_REQ_FK" FOREIGN KEY ("ID_HEADER_REQ")
	  REFERENCES "PE"."VACATION_HEADER_REQ" ("ID_HEADER_REQ") ENABLE;
        
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
      
    
    
    