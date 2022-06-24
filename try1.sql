select* from employee

CREATE table employees ( 
id smallint NOT NULL increment,
firstname varchar(30),
lastname varchar(30),
emailid varchar(30),
Primary key(no));

insert into employee (firstname,lastname,emailid) values ('a', 'b', 'c')

CREATE SEQUENCE sequence_sk_produk;
ALTER TABLE employee ALTER COLUMN id SET DEFAULT NEXTVAL('id')


CREATE table pelanggan ( 
id smallint NOT NULL primary key,
nama varchar(100),
no_telp varchar(30),
alamat varchar(20)
);

CREATE SEQUENCE id_pelanggan;
ALTER TABLE pelanggan ALTER COLUMN id SET DEFAULT NEXTVAL('id_pelanggan')
select* from p
select* from pelanggan order by NAMA
insert into pelanggan (nama, no_telp, alamat) values ('Caca', '08', 'sa')

update pelanggan set alamat='a' where id= 8

CREATE table baju ( 
	idb smallint NOT NULL primary key,
	id smallint REFERENCES pelanggan (id),
	l_dada integer (100),
	l_kerah integer (10),
	l_ujung_lengan integer(5),
	p_bahu integer(5),
	p_baju integer(5),
	p_baju integer(5)
);
CREATE TABLE baju (
  	idb INT NOT NULL,
  	id INT NOT NULL,
	l_dada int,
	l_kerah int,
	l_ujung_lengan int,
	p_bahu int,
	p_baju int,
	p_lengan int,
  PRIMARY KEY (idb),
  FOREIGN KEY (id)
      REFERENCES pelanggan (id)
);
CREATE SEQUENCE id_baju;
ALTER TABLE baju ALTER COLUMN idb SET DEFAULT NEXTVAL('id_baju')
select * from baju

SELECT idb, nama as nama_pelanggan, l_dada, l_kerah, l_ujung_lengan, p_bahu, p_baju, p_lengan FROM baju
INNER JOIN pelanggan
ON baju.id = pelanggan.id;

select * from pelanggan;

insert into baju (id, l_dada, l_kerah, l_ujung_lengan, p_bahu, p_baju, p_lengan) 
values (3,30,203,2,3,4,5)
SELECT name, date, roll
FROM A
INNER JOIN B
ON A.id = B.id)

CREATE TABLE celana (
  	idc INT NOT NULL,
  	id INT NOT NULL,
	l_paha int,
	l_pinggang int,
	l_pisak int,
	l_ujung_celana int,
	p_celana int,
  PRIMARY KEY (idc),
  FOREIGN KEY (id)
      REFERENCES pelanggan (id)
);

CREATE SEQUENCE id_celana;
ALTER TABLE celana ALTER COLUMN idc SET DEFAULT NEXTVAL('id_celana')
select * from celana
insert into celana (id, l_paha, l_pinggang, l_pisak, l_ujung_celana, p_celana) 
values (3,30,203,2,3,4)

SELECT idc, nama, l_paha, l_pinggang, l_pisak, l_ujung_celana, p_celana FROM celana 
INNER JOIN pelanggan ON celana.id = pelanggan.id; 

CREATE TABLE rok (
  	idr INT NOT NULL,
  	id INT NOT NULL,
	l_panggul int,
	l_pinggang int,
	p_rok int,
	t_panggul int,
  PRIMARY KEY (idr),
  FOREIGN KEY (id)
      REFERENCES pelanggan (id)
);

CREATE SEQUENCE id_rok;
ALTER TABLE rok ALTER COLUMN idr SET DEFAULT NEXTVAL('id_rok')
select * from rok
insert into rok (id, l_panggul, l_pinggang, p_rok, t_panggul) 
values (3,30,203,2,3)


CREATE TABLE pemesanan (
  	idp INT NOT NULL,
	id int not null,
	idb int null,
	idc int null,
	idr int null,
	status_pemesanan varchar(30),
	tgl_ambil varchar(30),
	tgl_pesan varchar(30),
  PRIMARY KEY (idp),
	FOREIGN KEY (id)
      REFERENCES pelanggan (id),
	FOREIGN KEY (idb)
      REFERENCES baju (idb),
	FOREIGN KEY (idc)
      REFERENCES celana (idc),
	FOREIGN KEY (idr)
      REFERENCES rok (idr)
);

CREATE SEQUENCE id_p;
ALTER TABLE pemesanan ALTER COLUMN idp SET DEFAULT NEXTVAL('id_p')

SELECT idp, baju.id, nama, baju.idb, status_pemesanan, tgl_pesan ,tgl_ambil FROM pemesanan 
INNER JOIN baju ON pemesanan.idb = baju.idb INNER JOIN pelanggan ON pelanggan.id = baju.id
INNER JOIN celana ON pelanggan.id = celana.id INNER JOIN rok ON pelanggan.id = rok.id; 
insert into pemesanan (id, idb, idc, idr, status_pemesanan, tgl_ambil, tgl_pesan) 
values (4, (select idb from baju where id = 4),(select idc from celana where id = 4),
		(select idr from rok where id = 4), 'selesai', '5 Feb 2022', '1 Feb 2022')

select nama, idb, idc,idr, status_pemesanan, tgl_ambil, tgl_pesan from pemesanan inner join pelanggan
on pemesanan.id = pelanggan.id

select * from pemesanan
select * from pelanggan
select * from baju
select * from rok
select * from celana

delete from pelanggan where id = 2;
delete from rok where idr =3 ;

insert into pemesanan (id, status_pemesanan, tgl_ambil, tgl_pesan) 
values (4, 'selesai', '5 Feb 2022', '1 Feb 2022')

SELECT idp, baju.id, nama, baju.idb, celana.idc, rok.idr, status_pemesanan, tgl_pesan ,tgl_ambil 
FROM pemesanan INNER JOIN baju ON pemesanan.idb = baju.idb INNER JOIN pelanggan ON pelanggan.id = pemesanan.id 
INNER JOIN celana ON pemesanan.id = celana.id INNER JOIN rok ON pemesanan.id = rok.id
where pelanggan.id = 4

select * from pemesanan
select * from pelanggan
select * from baju
select * from rok
select * from celana
