select num_aluno from Matricula where
	YEAR(data_inic) - YEAR(GETDATE()) >= 3 and data_conc is null and média is null


