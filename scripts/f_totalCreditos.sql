create function dbo.f_totalCreditos(@param_sig_curs varchar(6)) returns int
as
begin
		declare @returner int = (select sum(UC.num_cred)
		from UC 
		inner join UCdeCurso on Uc.sig_un = UCdeCurso.sig_uc
		inner join Curso on UCdeCurso.sig_curs = Curso.sig_un
		where Curso.sig_un = @param_sig_curs)
		return @returner
end
