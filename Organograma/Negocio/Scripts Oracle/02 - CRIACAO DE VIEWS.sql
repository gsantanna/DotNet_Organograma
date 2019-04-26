




/*
drop view ORG_LISTA_SECOES_SIMPLES ;  
drop view ORG_LISTA_SECOES_CONSOLIDADO;
drop view ORG_LISTA_SECOES;
drop view ORG_FUNCIONARIO_ALOCADO;
drop view ORG_FUNCIONARIO_EXPORT_GD;
drop view ORG_FUNCIONARIO_EXPORT_TR;
drop view ORG_LISTA_DIRETORIAS;
drop view ORG_LISTA_SUBSECAO;
drop view ORG_SECAO_GESTOR;
drop view ORG_FUNCIONARIO_DISPONIVEL;


alter view ORG_LISTA_SECOES_SIMPLES  compile;
alter view ORG_LISTA_SECOES_CONSOLIDADO compile;
alter view ORG_LISTA_SECOES compile;
alter view ORG_FUNCIONARIO_ALOCADO compile;
alter view ORG_FUNCIONARIO_EXPORT_GD compile;
alter view ORG_FUNCIONARIO_EXPORT_TR compile;
alter view ORG_LISTA_DIRETORIAS compile;
alter view ORG_LISTA_SUBSECAO compile;
alter view ORG_SECAO_GESTOR compile;
alter view ORG_FUNCIONARIO_DISPONIVEL compile;


*/







--------------------------------------------------------
--  DDL for View ORG_LISTA_SECOES_SIMPLES
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_LISTA_SECOES_SIMPLES" ("COD_SECAO", "COD_EMPRESA", "NOME_SECAO", "COD_EMPRESA_SUP", "COD_SECAO_SUP", "SECAO_SUP", "DIRETORIA", "PUBLICO", "CPF_GESTOR", "GESTOR") AS 
  select s.cod_secao, s.cod_empresa,
  case when  nvl(h.nome_mod,'')='1' then h.nome else s.nome_secao end
  NOME_SECAO, 
  h.cod_empresa_sup COD_EMPRESA_SUP,
  h.cod_secao_sup COD_SECAO_SUP,
  
  case when suph.NOME_MOD='1' then suph.nome else sup.nome_secao end 
    
   SECAO_SUP,
  nvl(h.diretoria,0)  Diretoria,  h.publico, f.cpf cpf_gestor, f.nome gestor
  
  from  org_secao s
  
  left outer join org_hierarquia h on s.cod_secao=h.cod_secao and s.cod_empresa=h.cod_empresa
  
  --Secao Superior
  left outer join org_secao sup on s.cod_secao=sup.cod_secao and s.cod_empresa=sup.cod_empresa 
  left outer join org_hierarquia suph on suph.cod_secao=sup.cod_secao and suph.cod_empresa=sup.cod_empresa

  --join para trazer os dados do gestor 
  left outer join org_funcionario f on f.cpf = 
nvl(case when (h.gestor_mod='1') then 
    h.gestor 
    else
    s.cpf_gestor 
    end
    , case when  suph.gestor_mod='1'  then suph.gestor else sup.cpf_gestor end);













--------------------------------------------------------
--  DDL for View ORG_LISTA_SECOES_CONSOLIDADO
--------------------------------------------------------

  CREATE OR REPLACE VIEW "ORG_LISTA_SECOES_CONSOLIDADO" ("COD_EMPRESA", "COD_SECAO", "NOME_FILIAL", "COD_FILIAL", "NOME_SECAO", "COD_SECAO_SUP", "COD_EMPRESA_SUP", "NOME_SECAO_SUP", "CPF_GESTOR", "NOME_GESTOR", "COD_DIRETORIA", "NOME_DIRETORIA", "TIPO", "POSSUI_SUPERIOR", "PUBLICO") AS select   h.cod_empresa, h.cod_secao , sdet.nome_filial, sdet.cod_filial, h.nome nome_secao,
         h.cod_secao_sup, h.cod_empresa_sup, s.nome_secao nome_secao_sup, 
    
    
         case when h.gestor_mod='1'  and h.gestor is not null  then h.gestor else  
        nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(
            s.cpf_gestor,  s2.cpf_gestor ), s3.cpf_gestor), s4.cpf_gestor), s5.cpf_gestor), 
 s6.cpf_gestor), s7.cpf_gestor), s8.cpf_gestor), s9.cpf_gestor), s10.cpf_gestor) 
         
         end cpf_gestor ,
         
         case when h.gestor_mod='1' and  h.gestor is not null  then f.nome  else 
         
          nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(
          s.gestor, s2.gestor ), s3.gestor), s4.gestor), s5.gestor), 
          s6.gestor), s7.gestor), s8.gestor), s9.gestor), s10.gestor) 
          end nome_gestor ,
          
  case when s.diretoria='1' then s.cod_secao 
  when s.diretoria='1' then  s.cod_secao 
  when s2.diretoria='1' then s2.cod_secao 
  when s3.diretoria='1' then s3.cod_secao 
  when s4.diretoria='1' then s4.cod_secao 
  when s5.diretoria='1' then s5.cod_secao 
  when s6.diretoria='1' then s6.cod_secao 
  when s7.diretoria='1' then s7.cod_secao 
  when s8.diretoria='1' then s8.cod_secao 
  when s9.diretoria='1' then s9.cod_secao 
  when s10.diretoria='1' then s10.cod_secao 
  end  cod_diretoria,
  
  case when s.diretoria='1' then s.nome_secao 
  when s.diretoria='1' then  s.nome_secao 
  when s2.diretoria='1' then s2.nome_secao 
  when s3.diretoria='1' then s3.nome_secao 
  when s4.diretoria='1' then s4.nome_secao 
  when s5.diretoria='1' then s5.nome_secao 
  when s6.diretoria='1' then s6.nome_secao 
  when s7.diretoria='1' then s7.nome_secao 
  when s8.diretoria='1' then s8.nome_secao 
  when s9.diretoria='1' then s9.nome_secao 
  when s10.diretoria='1' then s10.nome_secao 
  end  nome_diretoria
  
         ,'SUBSECAO' TIPO , h.possui_superior, h.publico

from org_hierarquia h
    inner join org_lista_secoes_simples s on h.cod_empresa_sup=s.cod_empresa and h.cod_secao_sup=s.cod_secao 
    inner join org_secao sdet on sdet.cod_empresa=s.cod_empresa and sdet.cod_secao=s.cod_secao 
    --join com superiores ate encontrar o gestor 
    --Secao
    left outer join org_lista_secoes_simples s2 on s2.cod_empresa=s.cod_empresa_sup and s2.cod_secao=s.cod_secao_sup
    left outer join org_lista_secoes_simples s3 on s3.cod_empresa=s2.cod_empresa_sup and s3.cod_secao=s2.cod_secao_sup  
    left outer join org_lista_secoes_simples s4 on s4.cod_empresa=s3.cod_empresa_sup and s4.cod_secao=s3.cod_secao_sup  
    left outer join org_lista_secoes_simples s5 on s5.cod_empresa=s4.cod_empresa_sup and s5.cod_secao=s4.cod_secao_sup  
    left outer join org_lista_secoes_simples s6 on s6.cod_empresa=s5.cod_empresa_sup and s6.cod_secao=s5.cod_secao_sup  
    left outer join org_lista_secoes_simples s7 on s7.cod_empresa=s6.cod_empresa_sup and s7.cod_secao=s6.cod_secao_sup  
    left outer join org_lista_secoes_simples s8 on s8.cod_empresa=s7.cod_empresa_sup and s8.cod_secao=s7.cod_secao_sup  
    left outer join org_lista_secoes_simples s9 on s9.cod_empresa=s8.cod_empresa_sup and s9.cod_secao=s8.cod_secao_sup  
    left outer join org_lista_secoes_simples s10 on s10.cod_empresa=s9.cod_empresa_sup and s10.cod_secao=s9.cod_secao_sup  

    left outer join org_funcionario f on f.cpf=h.gestor 
    where h.subsecao='1' 


   union all

select  
s.cod_empresa, s.cod_secao, se.nome_filial, se.cod_filial, s.nome_Secao, s.cod_secao_sup, s.cod_empresa_sup, s.secao_sup NOME_SECAO_SUP, 
 
 nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(
 s.cpf_gestor,  sup.cpf_gestor ), sup2.cpf_gestor ), sup3.cpf_gestor), sup4.cpf_gestor), sup5.cpf_gestor), 
 sup6.cpf_gestor), sup7.cpf_gestor), sup8.cpf_gestor), sup9.cpf_gestor), sup10.cpf_gestor) cpf_gestor,
 
 nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(nvl(
 s.gestor,  sup.gestor ), sup2.gestor ), sup3.gestor), sup4.gestor), sup5.gestor), 
 sup6.gestor), sup7.gestor), sup8.gestor), sup9.gestor), sup10.gestor) nome_gestor,
 
 
  case when s.diretoria='1' then s.cod_secao 
  when sup.diretoria='1' then  sup.cod_secao 
  when sup2.diretoria='1' then sup2.cod_secao 
  when sup3.diretoria='1' then sup3.cod_secao 
  when sup4.diretoria='1' then sup4.cod_secao 
  when sup5.diretoria='1' then sup5.cod_secao 
  when sup6.diretoria='1' then sup6.cod_secao 
  when sup7.diretoria='1' then sup7.cod_secao 
  when sup8.diretoria='1' then sup8.cod_secao 
  when sup9.diretoria='1' then sup9.cod_secao 
  when sup10.diretoria='1' then sup10.cod_secao 
  end  cod_diretoria,
  
  case when s.diretoria='1' then s.nome_secao 
  when sup.diretoria='1' then  sup.nome_secao 
  when sup2.diretoria='1' then sup2.nome_secao 
  when sup3.diretoria='1' then sup3.nome_secao 
  when sup4.diretoria='1' then sup4.nome_secao 
  when sup5.diretoria='1' then sup5.nome_secao 
  when sup6.diretoria='1' then sup6.nome_secao 
  when sup7.diretoria='1' then sup7.nome_secao 
  when sup8.diretoria='1' then sup8.nome_secao 
  when sup9.diretoria='1' then sup9.nome_secao 
  when sup10.diretoria='1' then sup10.nome_secao 
  end  nome_diretoria,
  

'SECAO' TIPO,  hhh.possui_superior, hhh.publico

from org_lista_secoes_simples s
   inner join org_secao se on se.cod_secao=s.cod_secao and se.cod_empresa=s.cod_empresa
   left outer join org_hierarquia hhh on hhh.cod_secao=s.cod_secao and hhh.cod_empresa=s.cod_empresa
   left outer join org_lista_secoes_simples sup on sup.cod_empresa=s.cod_empresa_sup and sup.cod_secao= s.cod_secao_sup
   left outer join org_lista_secoes_simples sup2 on sup2.cod_empresa=sup.cod_empresa_sup and sup2.cod_secao=sup.cod_secao_sup
   left outer join org_lista_secoes_simples sup3 on sup3.cod_secao=sup2.cod_secao_sup and sup3.cod_empresa=sup2.cod_empresa_sup
   left outer join org_lista_secoes_simples sup4 on sup4.cod_secao=sup3.cod_secao_sup and sup4.cod_empresa=sup3.cod_empresa_sup 
   left outer join org_lista_secoes_simples sup5 on sup5.cod_secao=sup4.cod_secao_sup and sup5.cod_empresa=sup4.cod_empresa_sup
   left outer join org_lista_secoes_simples sup6 on sup6.cod_secao=sup5.cod_secao_sup and sup6.cod_empresa=sup5.cod_empresa_sup
   left outer join org_lista_secoes_simples sup7 on sup7.cod_secao=sup6.cod_secao_sup and sup7.cod_empresa=sup6.cod_empresa_sup
   left outer join org_lista_secoes_simples sup8 on sup8.cod_secao=sup7.cod_secao_sup and sup8.cod_empresa=sup7.cod_empresa_sup
   left outer join org_lista_secoes_simples sup9 on sup9.cod_secao=sup8.cod_secao_sup and sup9.cod_empresa=sup8.cod_empresa_sup
   left outer join org_lista_secoes_simples sup10 on sup10.cod_secao=sup9.cod_secao_sup and sup10.cod_empresa=sup9.cod_empresa_sup
/






--------------------------------------------------------
--  DDL for View ORG_LISTA_SECOES
--------------------------------------------------------

  CREATE OR REPLACE VIEW "ORG_LISTA_SECOES" ("COD_EMPRESA", "COD_SECAO", "NOME_FILIAL", "COD_FILIAL", "NOME_RM", "NOME_MOD", "NOME_MODIFICADO", "NOME_SECAO_FINAL", "COD_SECAO_SUP", "COD_EMPRESA_SUP", "NOME_SECAO_SUP", "CPF_GESTOR_RM", "NOME_GESTOR_RM", "GESTOR_MOD", "CPF_GESTOR_MOD", "NOME_GESTOR_MOD", "DIRETORIA", "DIRETORIA_COD", "PUBLICO", "POSSUI_SUPERIOR") AS select 
s.cod_empresa, s.cod_secao , s.NOME_FILIAL, s.COD_FILIAL, 
s.NOME_SECAO NOME_RM, 
nvl(h.NOME_MOD,'0') NOME_MOD,
h.NOME NOME_MODIFICADO,
--se o nome foi modificado carrega o nome 
case when  nvl(h.NOME_MOD,'')='1' then h.NOME else s.NOME_SECAO end NOME_SECAO_FINAL,

--Secao superior 
sup.COD_SECAO cod_secao_sup,
sup.COD_EMPRESA cod_empresa_sup,
sup.NOME_SECAO  nome_secao_sup,
--GESTOR RM
f.cpf  CPF_GESTOR_RM, 
f.nome NOME_GESTOR_RM, 

nvl(h.GESTOR_MOD,'0') GESTOR_MOD,
f2.cpf CPF_GESTOR_MOD,
f2.nome NOME_GESTOR_MOD,

case
 when nvl( sup.diretoria,'')='1'   then  sup.NOME_SECAO
 when nvl( sup2.diretoria,'')='1'  then  sup2.NOME_SECAO
 when nvl( sup3.diretoria,'')='1'  then  sup3.NOME_SECAO
 when nvl( sup4.diretoria,'')='1'  then  sup4.NOME_SECAO
 when nvl( sup5.diretoria,'')='1'  then  sup5.NOME_SECAO
 when nvl( sup6.diretoria,'')='1'  then  sup6.NOME_SECAO
 when nvl( sup7.diretoria,'')='1'  then  sup7.NOME_SECAO
 when nvl( sup8.diretoria,'')='1'  then  sup8.NOME_SECAO
 when nvl( sup9.diretoria,'')='1'  then  sup9.NOME_SECAO
 when nvl( sup10.diretoria,'')='1' then  sup10.NOME_SECAO  
 when h.diretoria='1' then  case when  nvl(h.NOME_MOD,'')='1' then h.NOME else s.NOME_SECAO end
 
end   diretoria , 


 case
 when nvl( sup.diretoria,'')='1'   then  sup.COD_SECAO
 when nvl( sup2.diretoria,'')='1'  then  sup2.COD_SECAO
 when nvl( sup3.diretoria,'')='1'  then  sup3.COD_SECAO
 when nvl( sup4.diretoria,'')='1'  then  sup4.COD_SECAO
 when nvl( sup5.diretoria,'')='1'  then  sup5.COD_SECAO
 when nvl( sup6.diretoria,'')='1'  then  sup6.COD_SECAO
 when nvl( sup7.diretoria,'')='1'  then  sup7.COD_SECAO
 when nvl( sup8.diretoria,'')='1'  then  sup8.COD_SECAO
 when nvl( sup9.diretoria,'')='1'  then  sup9.COD_SECAO
 when nvl( sup10.diretoria,'')='1' then  sup10.COD_SECAO  
 when h.diretoria='1' then h.cod_secao 
 
 --a diretoria é no nivel acima do superior, carregar a diretoria  Sup2
end   diretoria_cod, 

nvl(
h.publico,'0')  publico,  nvl( h.possui_superior,'0') possui_superior

from org_secao s 
Left outer join org_hierarquia h on s.cod_secao=h.cod_secao and s.cod_empresa=h.cod_empresa
--join para o gestor 
left outer join org_funcionario f on f.CPF = s.CPF_GESTOR --GESTOR RH 
left outer join org_funcionario f2 on f2.cpf=h.GESTOR -- GESTOR MODIFICADO 

--join para trazer o superior 
left outer join org_lista_secoes_simples sup on sup.cod_secao=h.cod_secao_sup and sup.cod_empresa=h.cod_empresa_sup
left outer join org_lista_secoes_simples sup2 on sup2.cod_secao= sup.cod_secao_sup and sup2.cod_empresa=sup.cod_empresa_sup
left outer join org_lista_secoes_simples sup3 on sup3.cod_secao=sup2.cod_secao_sup and sup3.cod_empresa=sup2.cod_empresa_sup
left outer join org_lista_secoes_simples sup4 on sup4.cod_secao=sup3.cod_secao_sup and sup4.cod_empresa=sup3.cod_empresa_sup 
left outer join org_lista_secoes_simples sup5 on sup5.cod_secao=sup4.cod_secao_sup and sup5.cod_empresa=sup4.cod_empresa_sup
left outer join org_lista_secoes_simples sup6 on sup6.cod_secao=sup5.cod_secao_sup and sup6.cod_empresa=sup5.cod_empresa_sup
left outer join org_lista_secoes_simples sup7 on sup7.cod_secao=sup6.cod_secao_sup and sup7.cod_empresa=sup6.cod_empresa_sup
left outer join org_lista_secoes_simples sup8 on sup8.cod_secao=sup7.cod_secao_sup and sup8.cod_empresa=sup7.cod_empresa_sup
left outer join org_lista_secoes_simples sup9 on sup9.cod_secao=sup8.cod_secao_sup and sup9.cod_empresa=sup8.cod_empresa_sup
left outer join org_lista_secoes_simples sup10 on sup10.cod_secao=sup9.cod_secao_sup and sup10.cod_empresa=sup9.cod_empresa_sup
/





--------------------------------------------------------
--  DDL for View ORG_FUNCIONARIO_ALOCADO
--------------------------------------------------------

CREATE OR REPLACE FORCE VIEW "ORG_FUNCIONARIO_ALOCADO" ("NOME", "CPF", "COD_EMPRESA", "COD_SECAO", "COD_SECAO_SUP", "COD_EMPRESA_SUP") AS 
select  f.Nome,  f.cpf , s.cod_empresa,   s.cod_secao ,    s.cod_secao_sup , s.cod_empresa_sup 
from org_funcionario f 
left outer join org_funcionario_subsecao fs on f.cpf=fs.cpf
--carregar a hierarquia 
left outer join ORG_LISTA_SECOES_SIMPLES  s on  nvl( fs.cod_secao, f.cod_secao) = s.cod_secao and nvl(fs.cod_empresa, f.cod_empresa) = s.cod_empresa;






--------------------------------------------------------
--  DDL for View ORG_FUNCIONARIO_EXPORT_GD
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_FUNCIONARIO_EXPORT_GD" ("COD_FUNCIONARIO", "NUM_MATRICULA", "COD_EMPRESA", "COD_ORGAO", "COD_CARGO", "COD_SUPERVISOR", "DSC_DOMINIO", "DSC_LOGON", "DSC_SENHA", "NOM_FUNCIONARIO", "SEXO", "DAT_ADMISSAO", "DSC_EMAIL", "COD_TIPO_FUNC", "COD_TIPO_USUARIO", "DAT_NASCIMENTO", "COD_FILIAL", "COD_CPF", "DSC_APELIDO", "DAT_INICIO_FERIAS", "DAT_FIM_FERIAS", "COD_SUPERVISOR_FUNCIONAL", "DSC_GRADE_SALARIAL", "DAT_INICIO_CARGO", "COD_IDIOMA_PREF", "COD_PAIS", "COD_UF", "NOME_SECAO", "NOME_SECAO_SUP", "COD_SECAO_SUP", "COD_DIRETORIA", "NOME_DIRETORIA") AS 
  select  f.cpf COD_FUNCIONARIO,
f.chapa NUM_MATRICULA,
f.cod_empresa COD_EMPRESA,
fa.cod_secao COD_ORGAO,
f.cod_cargo , s.cpf_gestor   COD_SUPERVISOR,
'' DSC_DOMINIO,
f.login DSC_LOGON, '' DSC_SENHA, f.nome NOM_FUNCIONARIO, f.sexo,  f.data_admissao DAT_ADMISSAO,
f.email DSC_EMAIL,  to_char( f.cod_tipo_func) cod_tipo_func , '' COD_TIPO_USUARIO, f.data_nascimento DAT_NASCIMENTO,
f.cod_filial,
'' COD_CPF,
'' DSC_APELIDO,
'' DAT_INICIO_FERIAS ,
'' DAT_FIM_FERIAS,
'' COD_SUPERVISOR_FUNCIONAL,
'' DSC_GRADE_SALARIAL,
'' DAT_INICIO_CARGO ,
'' COD_IDIOMA_PREF,
'' COD_PAIS,
'' COD_UF,
s.nome_secao  NOME_SECAO,
s.nome_secao_sup,
s.COD_SECAO_SUP,
s.cod_diretoria cod_diretoria,
s.nome_diretoria
from org_funcionario f
left outer join org_funcionario_alocado fa on f.cpf=fa.cpf
left outer join org_lista_secoes_consolidado s on s.cod_secao=fa.cod_secao and s.cod_empresa=fa.cod_empresa;









--------------------------------------------------------
--  DDL for View ORG_FUNCIONARIO_EXPORT_TR
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_FUNCIONARIO_EXPORT_TR" ("FIRST_NAME", "LAST_NAME", "EMAIL", "USERNAME", "PASSWORD", "PERSONSTATUS", "USERSTATUS", "CPF", "COD_EMPRESA", "NOME_EMPRESA", "COD_FILIAL", "NOME_FILIAL", "DAT_ADMISSAO", "COD_SECAO", "NOME_SECAO", "CHAPA", "COD_CARGO", "NOME_FUNCAO", "CPF_GESTOR", "NOME_GESTOR", "EMAIL_GESTOR", "SEXO", "DATA_NASCIMENTO", "COD_TIPO_FUNC", "DESC_TIPO_FUNC", "COD_SECAO_SUP", "NOME_SECAO_SUP", "COD_DIRETORIA", "NOME_DIRETORIA", "PERFIL_PROFISSIONAL") AS 
  select 
SUBSTR(f.nome, 1, INSTR(f.nome,' ',1)) FIRST_NAME,
SUBSTR(f.nome, INSTR(f.nome,' ')) LAST_NAME,
f.Email, f.CPF  username, f.cpf  password, 
'ACTIVE' personstatus, 'ACTIVE' userstatus, 
f.CPF, f.COD_EMPRESA, f.Nome_empresa,f.COD_FILIAL,f.NOME_FILIAL,  to_char(f.DATA_ADMISSAO) DAT_ADMISSAO,
s.cod_secao, s.nome_secao, f.chapa , o.cod_cargo, o.dsc_cargo nome_funcao, gestor.cpf CPF_GESTOR, gestor.NOME NOME_GESTOR, gestor.email EMAIL_GESTOR, 


f.SEXO,  to_char(  f.DATA_NASCIMENTO  ) DATA_NASCIMENTO, 
f.cod_tipo_func, f.desc_tipo_func,
s.cod_secao_sup, s.nome_secao_sup ,   s.cod_diretoria, s.nome_diretoria , f.perfil_profissional
from  org_funcionario f 
inner join org_funcionario_alocado fa on f.cpf=fa.cpf
inner join org_lista_secoes_consolidado s on fa.cod_secao=s.cod_secao and fa.cod_empresa=s.cod_empresa
inner join org_cargo o on o.cod_cargo=f.cod_cargo
inner join org_funcionario gestor on gestor.cpf=s.cpf_gestor;






--------------------------------------------------------
--  DDL for View ORG_LISTA_DIRETORIAS
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_LISTA_DIRETORIAS" ("COD_EMPRESA", "COD_SECAO", "NOME_SECAO", "TT", "DIRETORIA") AS 
  select s.COD_EMPRESA, s.COD_SECAO, 
case when h.NOME_MOD='1' then --mudou o nome, pegar o nome da tabela hierarquia
h.NOME  else
s.NOME_SECAO end NOME_SECAO,
case when h.NOME_MOD='1' then --carrega o tooltip dizendo que o nome foi modificado! 
'Nome Original: ' || s.NOME_SECAO  else '' end TT ,  nvl(h.diretoria,'0') DIRETORIA
from ORG_SECAO s 
left outer join ORG_HIERARQUIA h on s.cod_empresa=h.cod_empresa and s.cod_secao=h.cod_secao and 
 (SUBSECAO <> '1' OR SUBSECAO is null);




--------------------------------------------------------
--  DDL for View ORG_LISTA_SUBSECAO
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_LISTA_SUBSECAO" ("COD_EMPRESA", "COD_SECAO", "NOME", "COD_SECAO_SUP", "NOME_SECAO_SUP", "GESTOR", "DIRETORIA", "DIRETORIA_COD") AS 
  select  
h.cod_empresa, h.cod_secao, h.nome , 
s.COD_SECAO COD_SECAO_SUP,
s.nome_secao_sup, nome_gestor gestor,
s.nome_diretoria  DIRETORIA,
s.cod_diretoria   DIRETORIA_COD
      from  ORG_Hierarquia h 

Inner join org_lista_secoes_consolidado s on h.cod_secao=s.cod_secao and h.cod_empresa=s.cod_empresa


where h.subsecao='1';







--------------------------------------------------------
--  DDL for View ORG_SECAO_GESTOR
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW "ORG_SECAO_GESTOR" ("COD_SECAO", "NOME_SECAO", "CPF_GESTOR", "NOME_GESTOR") AS 
  select h.cod_secao, h.nome nome_secao, nvl(f.cpf, f2.cpf) cpf_gestor,  nvl( 
f.nome , f2.nome) nome_gestor 
from org_hierarquia h 
left outer join org_funcionario f on f.cpf=h.gestor --GESTOR FOI SETADO 
--PEGAR O GESTOR DA LISTA DE SECOES (GESTOR PAI)
left outer join org_lista_secoes ls on ls.cod_secao=h.cod_secao_sup and 
ls.cod_empresa=h.cod_empresa_sup
 left outer join org_funcionario f2 on f2.cpf= case when nvl(ls.GESTOR_MOD,'')='1' then 
CPF_GESTOR_MOD else CPF_GESTOR_RM end
where subsecao='1'
union all 
select  cod_secao, ls.nome_secao_final nome_Secao  , 
case when ls.gestor_mod='1' then cpf_gestor_mod else cpf_gestor_rm end  cpf_gestor ,
case when ls.gestor_mod='1' then nome_gestor_mod else cpf_gestor_mod end nome_gestor 
from ORG_LISTA_SECOES ls;








--------------------------------------------------------
--  DDL for View ORG_FUNCIONARIO_DISPONIVEL
--------------------------------------------------------

  CREATE OR REPLACE FORCE VIEW  "ORG_FUNCIONARIO_DISPONIVEL" ("COD_SECAO", "NOME", "CPF") AS 
  select s.cod_secao , fa.nome, fa.cpf  from org_lista_secoes_simples s 
left outer join org_lista_secoes_simples sup on sup.cod_secao=s.cod_secao_sup
left outer join org_lista_secoes_simples sup2 on sup2.cod_secao=sup.cod_secao_sup
left outer join org_lista_secoes_simples sup3 on sup3.cod_secao=sup2.cod_secao_sup
left outer join org_lista_secoes_simples sup4 on sup4.cod_secao=sup3.cod_secao_sup 
left outer join org_lista_secoes_simples sup5 on sup5.cod_secao=sup4.cod_secao_sup 
left outer join org_lista_secoes_simples sup6 on sup6.cod_secao=sup5.cod_secao_sup 
left outer join org_lista_secoes_simples sup7 on sup7.cod_secao=sup6.cod_secao_sup 
left outer join org_lista_secoes_simples sup8 on sup8.cod_secao=sup7.cod_secao_sup 
left outer join org_lista_secoes_simples sup9 on sup9.cod_secao=sup8.cod_secao_sup
left outer join org_lista_secoes_simples sup10 on sup10.cod_secao=sup9.cod_secao_sup 
left outer join org_funcionario_alocado fa on fa.cod_secao in 

( s.cod_secao_sup, s.cod_secao, sup.cod_secao, sup2.cod_secao, sup3.cod_secao, sup4.cod_secao, sup5.cod_secao, sup6.cod_secao, sup7.cod_secao, sup8.cod_secao, sup9.cod_secao, sup10.cod_secao)
OR
fa.cod_secao_sup=   sup.cod_secao




order by nome;

















