
 
 
 


--SEQUENCE

--------------------------------------------------------
--  DDL for Sequence ORG_SEQ_LINHAS_AVULSAS
--------------------------------------------------------

   CREATE SEQUENCE  ORG_SEQ_LINHAS_AVULSAS  MINVALUE 1 MAXVALUE 999999999 INCREMENT 
BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;


--TABELAS



--------------------------------------------------------
--  DDL for Table ORG_FUNCIONARIO_EXPORT_GD_MDL
--------------------------------------------------------

  CREATE TABLE "ORG_FUNCIONARIO_EXPORT_GD_MDL" ("COD_FUNCIONARIO" VARCHAR2(50), "NUM_MATRICULA" VARCHAR2(50), "COD_EMPRESA" VARCHAR2(50), "COD_ORGAO" VARCHAR2(50), "COD_CARGO" VARCHAR2(50), "COD_SUPERVISOR" VARCHAR2(50), "DSC_DOMINIO" VARCHAR2(20), "DSC_LOGON" VARCHAR2(50), "DSC_SENHA" VARCHAR2(20), "NOM_FUNCIONARIO" VARCHAR2(100), "SEXO" VARCHAR2(1), "DAT_ADMISSAO" VARCHAR2(10), "DSC_EMAIL" VARCHAR2(160), "COD_TIPO_FUNC" VARCHAR2(1), "COD_TIPO_USUARIO" VARCHAR2(20), "DAT_NASCIMENTO" VARCHAR2(10), "COD_FILIAL" VARCHAR2(20), "COD_CPF" VARCHAR2(20), "DSC_APELIDO" VARCHAR2(40), "DAT_INICIO_FERIAS" VARCHAR2(10), "DAT_FIM_FERIAS" VARCHAR2(10), "COD_SUPERVISOR_FUNCIONAL" VARCHAR2(20), "DSC_GRADE_SALARIAL" VARCHAR2(20), "DAT_INICIO_CARGO" VARCHAR2(10), "COD_IDIOMA_PREF" VARCHAR2(20), "COD_PAIS" VARCHAR2(20), "COD_UF" VARCHAR2(20), "NOME_SECAO" VARCHAR2(90), "NOME_SECAO_SUP" VARCHAR2(90), "COD_SECAO_SUP" VARCHAR2(20), "COD_DIRETORIA" VARCHAR2(20), "NOME_DIRETORIA" VARCHAR2(90), "ID_LINHA" NUMBER)
/
--------------------------------------------------------
--  DDL for Table ORG_FUNCIONARIO_EXPORT_TR_MDL
--------------------------------------------------------

  CREATE TABLE "ORG_FUNCIONARIO_EXPORT_TR_MDL" ("FIRST_NAME" VARCHAR2(280), "LAST_NAME" VARCHAR2(280), "EMAIL" VARCHAR2(60), "USERNAME" VARCHAR2(11), "PASSWORD" VARCHAR2(11), "PERSONSTATUS" CHAR(6), "USERSTATUS" CHAR(6), "CPF" VARCHAR2(11), "COD_EMPRESA" VARCHAR2(1), "NOME_EMPRESA" VARCHAR2(60), "COD_FILIAL" VARCHAR2(2), "NOME_FILIAL" VARCHAR2(60), "DAT_ADMISSAO" VARCHAR2(10), "COD_SECAO" VARCHAR2(16), "NOME_SECAO" VARCHAR2(90), "CHAPA" VARCHAR2(5), "COD_CARGO" VARCHAR2(9), "NOME_FUNCAO" VARCHAR2(60), "CPF_GESTOR" VARCHAR2(11), "NOME_GESTOR" VARCHAR2(70), "EMAIL_GESTOR" VARCHAR2(60), "SEXO" VARCHAR2(1), "DATA_NASCIMENTO" VARCHAR2(10), "COD_TIPO_FUNC" VARCHAR2(1), "DESC_TIPO_FUNC" VARCHAR2(60), "COD_SECAO_SUP" VARCHAR2(20), "NOME_SECAO_SUP" VARCHAR2(90), "COD_DIRETORIA" VARCHAR2(20), "NOME_DIRETORIA" VARCHAR2(90), "ID_LINHA" NUMBER, "PERFIL_PROFISSIONAL" VARCHAR2(60))
/



--------------------------------------------------------
--  DDL for Table ORG_FUNCIONARIO_SUBSECAO
--------------------------------------------------------

  CREATE TABLE "ORG_FUNCIONARIO_SUBSECAO" ("CPF" VARCHAR2(13), "COD_EMPRESA" VARCHAR2(2), 
"COD_SECAO" VARCHAR2(16))
/
--------------------------------------------------------
--  DDL for Table ORG_HIERARQUIA
--------------------------------------------------------

  CREATE TABLE "ORG_HIERARQUIA" ("COD_EMPRESA" VARCHAR2(2), "COD_SECAO" VARCHAR2(16), 
"COD_EMPRESA_SUP" VARCHAR2(2), "COD_SECAO_SUP" VARCHAR2(13), "POSSUI_SUPERIOR" CHAR(1), "NOME" 
VARCHAR2(50), "NOME_MOD" CHAR(1), "GESTOR" VARCHAR2(13), "GESTOR_MOD" CHAR(1), "SUBSECAO" CHAR
(1), "DIRETORIA" CHAR(1), "PUBLICO" CHAR(1))
/
--------------------------------------------------------
--  DDL for Index ORG_FUNCIONARIO_EXPORT_GD_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ORG_FUNCIONARIO_EXPORT_GD_PK" ON "ORG_FUNCIONARIO_EXPORT_GD_MDL" 
("ID_LINHA")
/
--------------------------------------------------------
--  DDL for Index ORG_FUNCIONARIO_EXPORT_TR_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ORG_FUNCIONARIO_EXPORT_TR_PK" ON "ORG_FUNCIONARIO_EXPORT_TR_MDL" 
("ID_LINHA")
/
--------------------------------------------------------
--  DDL for Index ORG_FUNCIONARIO_SUBSECAO_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ORG_FUNCIONARIO_SUBSECAO_PK" ON "ORG_FUNCIONARIO_SUBSECAO" 
("COD_EMPRESA", "COD_SECAO", "CPF")
/
--------------------------------------------------------
--  DDL for Index ORG_HIERARQUIA_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "ORG_HIERARQUIA_PK" ON "ORG_HIERARQUIA" ("COD_EMPRESA", "COD_SECAO")
/
--------------------------------------------------------
--  Constraints for Table ORG_FUNCIONARIO_EXPORT_GD_MDL
--------------------------------------------------------

  ALTER TABLE "ORG_FUNCIONARIO_EXPORT_GD_MDL" ADD CONSTRAINT "ORG_FUNCIONARIO_EXPORT_GD_PK" 
PRIMARY KEY ("ID_LINHA") ENABLE;
  ALTER TABLE "ORG_FUNCIONARIO_EXPORT_GD_MDL" MODIFY ("ID_LINHA" NOT NULL ENABLE);


/
--------------------------------------------------------
--  Constraints for Table ORG_FUNCIONARIO_EXPORT_TR_MDL
--------------------------------------------------------

  ALTER TABLE "ORG_FUNCIONARIO_EXPORT_TR_MDL" ADD CONSTRAINT "ORG_FUNCIONARIO_EXPORT_TR_PK" 
PRIMARY KEY ("ID_LINHA") ENABLE;

ALTER TABLE "ORG_FUNCIONARIO_EXPORT_TR_MDL" MODIFY ("ID_LINHA" NOT NULL ENABLE);
  
  
  
/
--------------------------------------------------------
--  Constraints for Table ORG_FUNCIONARIO_SUBSECAO
--------------------------------------------------------

  ALTER TABLE "ORG_FUNCIONARIO_SUBSECAO" ADD CONSTRAINT "ORG_FUNCIONARIO_SUBSECAO_PK" PRIMARY 
KEY ("COD_EMPRESA", "COD_SECAO", "CPF") ENABLE;
  ALTER TABLE "ORG_FUNCIONARIO_SUBSECAO" MODIFY ("CPF" NOT NULL ENABLE);
  ALTER TABLE "ORG_FUNCIONARIO_SUBSECAO" MODIFY ("COD_SECAO" NOT NULL ENABLE);
  ALTER TABLE "ORG_FUNCIONARIO_SUBSECAO" MODIFY ("COD_EMPRESA" NOT NULL ENABLE);
/
--------------------------------------------------------
--  Constraints for Table ORG_HIERARQUIA
--------------------------------------------------------

  ALTER TABLE "ORG_HIERARQUIA" ADD CONSTRAINT "ORG_HIERARQUIA_PK" PRIMARY KEY ("COD_EMPRESA", 
"COD_SECAO") ENABLE;
  ALTER TABLE "ORG_HIERARQUIA" MODIFY ("COD_SECAO" NOT NULL ENABLE);
  ALTER TABLE "ORG_HIERARQUIA" MODIFY ("COD_EMPRESA" NOT NULL ENABLE);
/







--------------------------------------------------------
--  TRIGGERS! 
--------------------------------------------------------


create or replace 
trigger ORG_EXPORT_TR_PK_SQ  
   before insert on "ORG_FUNCIONARIO_EXPORT_TR_MDL" 
   for each row 
begin  
   if inserting then 
         select ORG_SEQ_LINHAS_AVULSAS.nextval into :NEW."ID_LINHA" from dual; 
   end if; 
end;

/






create or replace 
TRIGGER "ORG_EXPORT_GD_PK_SQ" before insert on "ORGANOGRAMA_USER"."ORG_FUNCIONARIO_EXPORT_GD_MDL" 
   for each row 
begin  
   if inserting then 
         select ORG_SEQ_LINHAS_AVULSAS.nextval into :NEW."ID_LINHA" from dual; 
   end if; 
end;

/











