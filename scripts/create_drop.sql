--create database Instituto

use Instituto

create table Departamento(
	sig_un		varchar(6) primary key,
	descr		text	
);

create table Curso(
	sig_un		varchar(6) primary key,
	descr		text,
	sig_dep		varchar(6) references Departamento(sig_un)
);

create table Aluno(
	num_aluno	int IDENTITY(1, 1) primary key,
	num_cc		varchar(8) not null unique,
	nome		varchar(255),
	endereço	varchar(255),
	cod_post	varchar(8),
	localidade	varchar(255),
	data_nasc	date
);

create table UC(
	sig_un		varchar(6) primary key,
	num_cred	int, 
	descr		text
);

create table UCdeCurso (
	sig_uc		varchar(6) references UC(sig_un),
	sig_curs	varchar(6) references Curso(sig_un),
	ano			int,
	semestre	int check(semestre <= 6),
	primary key (sig_uc, sig_curs)

);

create table Ano(
	ano			int, 
	semestre	int check (semestre in (1, 2)), 
	sig_curs	varchar(6),
	primary key (ano, semestre, sig_curs)
);

create table Inscrição(
	ano			int, 
	nota		int check (nota <= 20),
	num_aluno	int references Aluno(num_aluno),
	sig_uc		varchar(6) references UC(sig_un),
	primary key (ano, num_aluno, sig_uc)
);

create table Secção(
	sig_un		varchar(6), 
	descr		text, 
	sig_dep		varchar(6) references Departamento(sig_un), 
	primary key (sig_un, sig_dep)
);

create table Professor(
	num_cc		varchar(8) primary key,
	nome		varchar(255), 
	area_esp	text,
	categoria	varchar(255), 
	sig_sec		varchar(6), 
	sig_dep		varchar(6), 
	coord_sec	varchar(6),
	foreign key (sig_sec, sig_dep) references Secção(sig_un, sig_dep),
	foreign key (coord_sec, sig_dep) references Secção(sig_un, sig_dep)
);

create table Ensina(
	sig_uc		varchar(6) references UC(sig_un),
	prof_cc		varchar(8) references Professor(num_cc),
	ano			int,
	primary key (sig_uc, prof_cc)
);

create table Matricula(
	num_aluno	int references Aluno(num_aluno),
	sig_curs	varchar(6) references Curso(sig_un),
	data_inic	date not null,
	data_conc	date,
	média		float,
	primary key (num_aluno, sig_curs)
);



drop table Matricula;
drop table Ensina;
drop table Professor;
drop table Secção;
drop table Inscrição;
drop table Ano;
drop table UCdeCurso;
drop table UC;
drop table Aluno;
drop table Curso;
drop table Departamento;


