DROP TABLE IF EXISTS Autor;
CREATE TABLE Autor(
    Id integer,
    Nombre text,
    Apellido text,
    FechaNacimiento timestamp without time zone,
    AutorGuid text,
    PRIMARY KEY(Id)
);

DROP TABLE IF EXISTS GradosAcademico;
CREATE TABLE GradosAcademico(
    Id integer,
    Nombre text,
    CentroAcademico text,
    FechaGrado timestamp without time zone,
    AutorId integer,
    GradoAcademicoGuid text,
    PRIMARY KEY(Id)
);
