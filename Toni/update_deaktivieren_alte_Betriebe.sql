

update betrieb set aktiv_jn = 'n'


from   betrieb, pj_bt, schue_sj
where pj_bt.pj_id = schue_sj.pj_id
and   betrieb.bt_id=pj_bt.bt_id
and   betrieb.bt_id not in (select Betrieb.bt_id       
                            from   betrieb, pj_bt, schue_sj
                            where  pj_bt.pj_id = schue_sj.pj_id
                            and betrieb.bt_id=pj_bt.bt_id
                            and schue_sj.vorgang_schuljahr in ('2006/07','2007/08','2008/09','2009/10','2010/11','2011/12')
                           )
;

