CREATE OR REPLACE FUNCTION coink_app.sp_create_user(
    p_full_name       VARCHAR,
    p_phone           VARCHAR,
    p_address         VARCHAR,
    p_country_id      INT,
    p_department_id   INT,
    p_municipality_id INT
)
RETURNS INT
LANGUAGE plpgsql
AS $$
DECLARE
    v_new_id INT;
BEGIN   
    IF NOT EXISTS (SELECT 1 FROM coink_app.country WHERE id = p_country_id) THEN
        RAISE EXCEPTION 'Country not found';
    END IF;

    IF NOT EXISTS (SELECT 1 FROM coink_app.department WHERE id = p_department_id) THEN
        RAISE EXCEPTION 'Department not found';
    END IF;

    IF NOT EXISTS (SELECT 1 FROM coink_app.municipality WHERE id = p_municipality_id) THEN
        RAISE EXCEPTION 'Municipality not found';
    END IF;

    INSERT INTO coink_app."user"(
        full_name, phone, address,
        country_id, department_id, municipality_id
    )
    VALUES (
        p_full_name, p_phone, p_address,
        p_country_id, p_department_id, p_municipality_id
    )
    RETURNING id INTO v_new_id;

    RETURN v_new_id;
END;
$$;

CREATE OR REPLACE FUNCTION coink_app.sp_get_user_by_id(p_user_id INT)
RETURNS TABLE (
    Id              INT,
    FullName       VARCHAR,
    Phone          VARCHAR,
    Address        VARCHAR,
    CountryId      INT,
    DepartmentId   INT,
    MunicipalityId INT,
    CreatedAt      TIMESTAMP
)
LANGUAGE sql
AS $$
    SELECT
        u.id           AS "Id",
        u.full_name    AS "FullName",
        u.phone        AS "Phone",
        u.address      AS "Address",
        u.country_id   AS "CountryId",
        u.department_id AS "DepartmentId",
        u.municipality_id AS "MunicipalityId",
        u.created_at   AS "CreatedAt"
    FROM coink_app."user" u
    WHERE u.id = p_user_id;
$$;

CREATE OR REPLACE FUNCTION coink_app.sp_get_countries()
RETURNS TABLE (
    Id INT,
    Name VARCHAR,
    IsoCode VARCHAR
)
LANGUAGE sql
AS $$
    SELECT
        c.id       AS "Id",
        c.name     AS "Name",
        c.iso_code AS "IsoCode"
    FROM coink_app.country c;
$$;

CREATE OR REPLACE FUNCTION coink_app.sp_get_departments_by_country(p_country_id INT)
RETURNS TABLE (
    Id INT,
    Name VARCHAR,
    CountryId INT
)
LANGUAGE sql
AS $$
    SELECT
        d.id          AS "Id",
        d.name        AS "Name",
        d.country_id  AS "CountryId"
    FROM coink_app.department d
    WHERE d.country_id = p_country_id;
$$;

CREATE OR REPLACE FUNCTION coink_app.sp_get_municipalities_by_department(p_department_id INT)
RETURNS TABLE (
    Id INT,
    Name VARCHAR,
    DepartmentId INT
)
LANGUAGE sql
AS $$
    SELECT
        m.id            AS "Id",
        m.name          AS "Name",
        m.department_id AS "DepartmentId"
    FROM coink_app.municipality m
    WHERE m.department_id = p_department_id;
$$;