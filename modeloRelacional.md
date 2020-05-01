# Modelo relacional

Departamento(**sig_un**, desc)

Curso(**sig_un**, desc, sig_dep)

Aluno(**num_aluno**, num_cc, nome, endereço, cod_post, localidade, data_nasc)

Secção(**sig_un**, desc, **sig_dep**)

Professor(**num_cc**, nome, area_esp, sig_secção, coord_sec)

UC(**sig_un**, num_creditos, desc, ano, semestre)

Ano(**ano**, **semestre**, **sig_curso**)

Inscrição(**ano**, nota, **num_aluno**, **sig_uc**)

Ensina(**sig_uc**, **num_cc**, ano)

Conclui(**num_aluno**, **sig_curso**, data, média)