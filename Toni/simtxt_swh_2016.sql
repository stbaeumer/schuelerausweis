SELECT   schueler.pu_id,
			schueler.name_1,
			schueler.name_2,
			check_null (hole_schuljahr_rech ('', -1))												as schuljahr_vorher, 
			check_null (hole_schuljahr_rech ('', 0))												as Bezugsjahr,				/*  1 aktuelles SJ des Users */
			check_null (sv_km_kuerzel_wert ('s_typ_vorgang', schue_sj.s_typ_vorgang))	as Status,  				/*  2 */
			/* lfd_nr im cf_ realisiert */  																							/*  3 */ 
			(if check_null (klasse.klasse_statistik_name) <> ''	THEN
				 check_null (klasse.klasse_statistik_name)         ELSE
				 check_null (klasse.klasse)								ENDIF)				as Klasse,  					/*  4 */

         check_null (sv_km_kuerzel_wert ('s_berufs_nr_gliederung', 
                                 schue_sj.s_berufs_nr_gliederung))   				                        as Gliederung,  			/*  5 */   
         check_null (substr(schue_sj.s_berufs_nr,4,5))				  					                    as Fachklasse,  			/*  6 Stelle 4-8 */  
			''																								as Klassenart,				/*  7 nicht BK */
			check_null (sv_km_kuerzel_wert ('s_klasse_art', klasse.s_klasse_art))	                        as OrgForm,  			    /*  8 */
         check_null (sv_km_kuerzel_wert ('jahrgang', schue_sj.s_jahrgang))			                        as AktJahrgang,  		    /*  9 */   
			check_null (sv_km_kuerzel_wert ('s_art_foerderungsbedarf', schueler.s_art_foerderungsbedarf))
																											as Foerderschwerp,			/* 10 */ 
			(if check_null (schueler.s_schwerstbehindert) = ''	THEN
				 '0'                 									ELSE
				 '1'															ENDIF)					    as Schwerstbeh,			   	/* 11 */ 
			''																							    as Reformpdg,				/* 12 */ 
			(if sv_steuerung ('s_unter', schueler.s_unter) = '$JVA' THEN
				 '1'                 									ELSE
				 '0'															ENDIF)		 			    as JVA,						/* 13 */ 
         adresse.plz	  						                             							    as Plz, 			 		/* 14 Neu 2016 */ 
		 hole_schueler_ort_bundesland (adresse.s_gem_kz, adresse.ort)				                        as Ort,			            /* 15 */
         schueler.dat_geburt			                    									            as Gebdat,      	        /* 16 */
         check_null (sv_km_kuerzel_wert ('s_geschl' , schueler.s_geschl))			                        as Geschlecht,              /* 17 */   
         check_null (sv_km_kuerzel_wert ('s_staat'  , schueler.s_staat))			                        as Staatsang, 				/* 18 */
         
		   check_null (sv_km_kuerzel_wert ('s_bekennt', schueler.s_bekennt))                                as Religion,  				/* 19 */
			schue_sj.dat_rel_anmeld																	        as Relianmeldung,			/* 20 */
			schue_sj.dat_rel_abmeld																	        as Reliabmeldung,			/* 21 */

			(if Aufnahmedatum_Bildungsgang is null THEN     		
				 Aufnahmedatum_Schule					ELSE
				 Aufnahmedatum_Bildungsgang			ENDIF)        							                as	Aufnahmedatum,   		/* 22 */  

			(select max (lehr_sc.ls_kuerzel)
            from lehr_sc, kl_ls, lehrer
        	  where lehr_sc.ls_id 		= kl_ls.ls_id                                                   
        		 and kl_ls.kl_id 			= klasse.kl_id
				 and kl_ls.s_typ_kl_ls 	= '0'
             and lehrer.le_id 		= lehr_sc.le_id) 										                as labk,					/* 23 */
			
			(select adresse.plz
			   from adresse, betrieb, pj_bt  
			  where adresse.ad_id 		= betrieb.id_hauptadresse  
				 and pj_bt.bt_id 			= betrieb.bt_id    
				 and pj_bt.s_typ_pj_bt 	= '0'       
				 and pj_bt.pj_id 			= schue_sj.pj_id)										        as ausbildort,  			/* 24 PLZ des Ausbildungsortes */                       
				
			hole_schueler_betriebsort (schue_sj.pj_id, 'ort')								                as betriebsort,  			/* 25 */                       
				
			/* Kapitel der zuletzt besuchten Schule */
         check_null (sv_km_kuerzel_wert ('s_herkunfts_schule', 
                                 schue_sj.s_herkunfts_schule))                                              as LSSchulform,             /* 26 */  
			left (schue_sj.vo_s_letzte_schule, 6) 						      		  		                as LSSchulnummer,			/* 27 */
			check_null (sv_km_kuerzel_wert ('s_berufl_vorbildung_glied', 
                                 schue_sj.s_berufl_vorbildung_glied))        		                        as LSGliederung,   		    /* 28 */  
			substr (schue_sj.s_berufl_vorbildung, 4, 5) 			   						                as LSFachklasse,   			/* 29 */  

			''																								as LSKlassenart,			/* 30 nicht BK */
			''																								as LSReformpdg,				/* 31 */
			Null																							as LSSchulentl,				/* 32 */		

			check_null (sv_km_kuerzel_wert ('s_abgang_jg', schue_sj.vo_s_jahrgang))							as	LSJahrgang,   			/* 33 */  

			(if Gliederung || Fachklasse = VOGliederung || VOFachklasse then 
             (if AktJahrgang = vojahrgang and Schueler_le_schuljahr_da = 'J' then 'W' else 'V' endif)   else
		      right (check_null (sv_km_kuerzel_wert ('s_schulabschluss', schue_sc.s_hoechst_schulabschluss)), 1) ENDIF)
																             							    as LSQUal,    				/* 34 */

			'0'																							    as LSVersetz,				/* 35 */ 
	 
			/* Kapitel für das abgelaufene Schuljahr */
			check_null ((if (Fall_Bezugsjahr = '1')				THEN
				(SELECT klasse.klasse									/* aus Vorjahr lesen */  
					FROM klasse, schue_sj  
				  WHERE schue_sj.kl_id = klasse.kl_id     
					 and schue_sj.pj_id = VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				klasse.klasse												ENDIF))				 	        as VOKlasse,    		    /* 36 */

			check_null ((if (Fall_Bezugsjahr = '1') 				THEN
				(select schue_sj.s_berufs_nr_gliederung			/* aus Vorjahr lesen */
					from schue_sj
				  where schue_sj.pj_id 	= VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				schue_sj.s_berufs_nr_gliederung						ENDIF)) 					           as VOGliederung,  			/* 37 */

			check_null ((if (Fall_Bezugsjahr = '1') 				THEN
				(select substr (schue_sj.s_berufs_nr, 4, 5)		/* aus Vorjahr lesen */ 
					from schue_sj
				  where schue_sj.pj_id 	= VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				substr (schue_sj.s_berufs_nr, 4, 5)					ENDIF)) 					           as VOFachklasse, 			/* 38 */

			check_null ((if (Fall_Bezugsjahr = '1')				THEN
				(SELECT sv_km_kuerzel_wert ('s_klasse_art', klasse.s_klasse_art)/* aus Vorjahr lesen */  
					FROM klasse, schue_sj  
				  WHERE schue_sj.kl_id = klasse.kl_id     
					 and schue_sj.pj_id = VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				sv_km_kuerzel_wert ('s_klasse_art', klasse.s_klasse_art) ENDIF))		                   as VOOrgForm,    			/* 39 */
			''																							   as VOKlassenart,				/* 40 nicht BK*/

			check_null ((if (Fall_Bezugsjahr = '1')				THEN
				(SELECT sv_km_kuerzel_wert ('jahrgang', schue_sj.s_jahrgang)/* aus Vorjahr lesen */  
					FROM klasse, schue_sj  
				  WHERE schue_sj.kl_id = klasse.kl_id     
					 and schue_sj.pj_id = VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				sv_km_kuerzel_wert ('jahrgang', schue_sj.s_jahrgang) ENDIF))		                       as VOJahrgang,    			/* 41 */

			check_null (sv_km_kuerzel_wert ('s_art_foerderungsbedarf_vj', 
                                 schueler.s_art_foerderungsbedarf_vj))   			                       as VOFoerderschwerp,			/* 42 */ 
			'0'																							   as VOSchwerstbeh,			/* 43 */ 
			''																							   as VOReformpdg,				/* 44 */
				
			hole_schueler_bildungsgang (schueler.pu_id, klasse.s_bildungsgang, schue_sj.dat_austritt, 'austritt')
																										   as EntlDatum,				/* 45 */				

			check_null (sv_km_kuerzel_wert ('s_austritts_grund_bdlg', 
                                 schue_sj.s_austritts_grund_bdlg))                                         as Zeugnis_JZ, 				/* 46 */

			''																								as Schulpflichterf,			/* 47 nicht BK */ 			

			''																								as Schulwechselform,		/* 48 nicht BK */ 		

			''																								as Versetzung,				/* 49 */ 		

			(IF s_geburts_land is not null AND s_geburts_land <> '000' THEN
             string (year (dat_zuzug))
          ELSE
             ''
          ENDIF
			)																								as JahrZuzug_nicht_in_D_geboren,	/* 50 */

			string (year (dat_eintritt_gs))														            as JahrEinschulung,					/* 51 */

			''																								as JahrWechselSekI,					/* 52 */

			(IF (s_geburts_land is not null AND s_geburts_land <> '000') OR (s_geburts_land is null AND dat_zuzug is not null) THEN
             '1'
          ELSE
             '0'
          ENDIF
			)																							    as Zugezogen,						/* 53 */


			/* Hilfsfelder */
			(SELECT max (adr_mutter.ad_id) 
            FROM adresse adr_mutter 
           WHERE adr_mutter.pu_id		= schueler.pu_id
             AND adr_mutter.s_typ_adr	= 'M'
			)																								as ad_id_mutter,

			(SELECT adr_mutter.s_herkunftsland_adr 
            FROM adresse adr_mutter 
           WHERE adr_mutter.ad_id		= ad_id_mutter
			)																								as herkunftsland_mutter,			/* 54 */

			(SELECT max (adr_vater.ad_id) 
            FROM adresse adr_vater
           WHERE adr_vater.pu_id			= schueler.pu_id
             AND adr_vater.s_typ_adr	= 'V'
			)																								as ad_id_vater,

			(SELECT adr_vater.s_herkunftsland_adr 
            FROM adresse adr_vater
           WHERE adr_vater.ad_id			= ad_id_vater
			)																								as herkunftsland_vater,				/* 55 */

			(IF herkunftsland_mutter is not null AND herkunftsland_mutter <> '000'
             OR
				 herkunftsland_vater  is not null AND herkunftsland_vater  <> '000' THEN
             '1'
          ELSE
             '0'
          ENDIF
         )																								    as Elternteilzugezogen,				/* 56 */

			(IF schueler.s_muttersprache is not null AND 
             schueler.s_muttersprache <> 'DE'     AND 
             schueler.s_muttersprache <> '000' THEN
             schueler.s_muttersprache
          ELSE
             ''
          ENDIF
         )																								    as Verkehrssprache,					/* 57 */

			''																								as Einschulungsart,					/* 58 nicht BK */
			''																								as Grundschulempfehlung,			/* 59 nicht BK */

			schue_sj.pj_id																				    as pj_id,
			check_null (sv_km_kuerzel_wert ('s_religions_unterricht', 
                                 schue_sj.s_religions_unterricht))					                        as s_religions_unterricht,
     		schue_sc.dat_eintritt	                   										                as Aufnahmedatum_schule,   			

			hole_schueler_bildungsgang (schueler.pu_id, klasse.s_bildungsgang, schue_sj.dat_eintritt, 'eintritt')
							     			                   										        as Aufnahmedatum_Bildungsgang,

			(SELECT max (pj_id) 
			   FROM schue_sj  
			  WHERE schue_sj.pu_id 					= schueler.pu_id 		and
					  schue_sj.s_typ_vorgang 		IN ('A', 'G', 'S')	and
					  schue_sj.vorgang_schuljahr 	= schuljahr_vorher) 						            as VOpj_id,

			test_austritt (schue_sj.dat_austritt)												            as ausgetreten,
		
			check_null ((SELECT noten_kopf.s_bestehen_absprf || noten_kopf.s_zusatzabschluss
			   FROM noten_kopf, schue_sj  
			  WHERE noten_kopf.s_typ_nok 			= 'HZ'
				 AND schue_sj.pj_id 					= noten_kopf.pj_id
				 AND schue_sj.pj_id 					= VOpj_id))            					            as Zeugnis_HZ,


			check_null ((if ( schue_sj.vorgang_schuljahr 	= check_null (hole_schuljahr_rech ('',  0)) /* '2005/06' */
	  				AND schue_sj.vorgang_akt_satz_jn = 'J'
	  				AND ausgetreten 						= 'N') 		THEN
				 '1'                 									ELSE
				 '0'															ENDIF))					    as Fall_Bezugsjahr,			

			check_null ((if ( schue_sj.vorgang_schuljahr 	= check_null (hole_schuljahr_rech ('', -1)) /* '2004/05' */
				   AND schue_sj.s_typ_vorgang 	IN ('A', 'G', 'S')
				   AND ausgetreten 					= 'J') 			THEN
				 '1'                 									ELSE
				 '0'															ENDIF))					    as Fall_schuljahr_vorher,
			
			check_null ((if ( VOJahrgang <> '')							 			THEN
				 'J'                 									ELSE
				 'N'															ENDIF))					    as Schueler_le_schuljahr_da,	
			
			check_null ((if ( Fachklasse <> VOFachklasse
					OR Gliederung <> VOGliederung)		 			THEN
				 'J'                 									ELSE
				 'N'															ENDIF))					    as Schueler_Berufswechsel,	
			
			check_null ((if ( VoGliederung = 'C05' 
				  AND Gliederung = 'C06')		 						THEN
				 'J'                 									ELSE
				 'N'															ENDIF))					    as Vorjahr_C05_Aktjahr_C06,		
			
			check_null (sv_km_kuerzel_wert ('jahrgang', schue_sj.s_jahrgang))				                as schueler_jahrgang, 

			check_null ((if (Fall_Bezugsjahr = '1') 				THEN
				(select schue_sj.s_jahrgang			/* aus Vorjahr lesen */
					from schue_sj
				  where schue_sj.pj_id 	= VOpj_id)					ELSE
				/* Fall_schuljahr_vorher */
				schue_sj.s_jahrgang										ENDIF)) 					        as VOSchueler_Jahrgang,

  			(IF EXISTS (SELECT 1 FROM schue_sj_info  
                      WHERE schue_sj_info.info_gruppe = 'MASSNA'
						      AND schue_sj_info.pj_id = schue_sj.pj_id) THEN '1' ELSE '0' ENDIF)            as Massnahmetraeger,	               /* 60 */

         check_null (sv_km_kuerzel_wert ('s_geschl', schueler.s_betreuungsart))	                            as Betreuung,   	                   /* 61 */   

  			(IF EXISTS (SELECT 1 FROM schueler_info  
                      WHERE schueler_info.info_gruppe = 'PUBEM'
                        AND schueler_info.s_typ_puin  = 'BKAZVO'
								AND schueler_info.betrag IN (0, 6, 12, 18)
						      AND schueler_info.pu_id = schueler.pu_id) THEN 
		   (SELECT konv_dec_string(schueler_info.betrag, 0)
			   FROM schueler_info  
			  WHERE schueler_info.puin_id = (SELECT max(puin.puin_id)  
														  FROM schueler_info puin 
														 WHERE puin.info_gruppe = 'PUBEM' 
														   AND puin.s_typ_puin = 'BKAZVO'
														   AND puin.betrag IN (0, 6, 12, 18)
														   AND puin.pu_id  = schueler.pu_id)) ELSE '0' ENDIF) as BKAZVO,	                       /* 62 */   
			check_null (sv_km_kuerzel_wert ('s_art_foerderungsbedarf2', 
                                 schueler.s_art_foerderungsbedarf2))  	                                      as Foerderschwerp2,		           /* 63 */   
			check_null (sv_km_kuerzel_wert ('s_art_foerderungsbedarf_vj2',
                                 schueler.s_art_foerderungsbedarf_vj2))                                       as VOFoerderschwerp2,				   /* 64 */   
			(IF schue_sc.berufsabschluss_jn = 'J' THEN 'Y' ELSE '' ENDIF)	                                  as Berufsabschluss, 			       /* 65 */   

			'Atlantis' 																		                  as Produktname, 					   /* 66 */   
			(SELECT 'V' || version.version_nr  
			   FROM version
           WHERE version.datum  = (SELECT max(v.datum) FROM version v))                                       as Produktversion,     			   /* 67 */  
           check_null (sv_km_kuerzel_wert ('s_bereich', klasse.s_bereich))                                    as Adressmerkmal,                    /* 68 */
 

			(if sv_steuerung ('s_unter', schueler.s_unter) = '$IN' THEN
				 '1'                 									ELSE
				 ''															ENDIF)		                      as Internat,						   /* 69 */ 
         '0'																			                      as Koopklasse                        /* 70 */

  FROM   schueler,   
         schue_sc,  
         schue_sj,   
			schule,
         klasse,   
         adresse

 WHERE   schue_sj.kl_id 					= klasse.kl_id    			     
     AND schue_sc.sc_id 					= schule.sc_id  			    
     AND schue_sc.ps_id 					= schue_sj.ps_id  			     
     AND schue_sc.pu_id 					= schueler.pu_id  			     
	  AND adresse.ad_id 						= schueler.id_hauptadresse  
     AND (
			 (schue_sj.vorgang_schuljahr 	= check_null (hole_schuljahr_rech ('',  0)) /* aktuelles Jahr */
	  AND   schue_sj.vorgang_akt_satz_jn 	= 'J'
	  AND   ausgetreten 						   = 'N'
			 )
          OR
          (
			  schue_sj.vorgang_schuljahr 	= check_null (hole_schuljahr_rech ('', -1)) /* letzte jahr */
	  AND   schue_sj.s_typ_vorgang 			IN ('G', 'S')
	  AND   ausgetreten = 'J' 
			 )
         ) 
ORDER BY ausgetreten DESC, klasse, schueler.name_1, schueler.name_2
