
CREATE DATABASE "CoinkApp";

CREATE SCHEMA IF NOT EXISTS coink_app;

CREATE TABLE IF NOT EXISTS coink_app.country (
    id          SERIAL PRIMARY KEY,
    name        VARCHAR(100) NOT NULL,
    iso_code    VARCHAR(5)   NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS coink_app.department (
    id          SERIAL PRIMARY KEY,
    country_id  INT NOT NULL,
    name        VARCHAR(100) NOT NULL
);

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.table_constraints tc
        WHERE tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'coink_app'
          AND tc.table_name   = 'department'
          AND tc.constraint_name = 'fk_department_country'
    ) THEN
        ALTER TABLE coink_app.department
        ADD CONSTRAINT fk_department_country
        FOREIGN KEY (country_id)
        REFERENCES coink_app.country(id);
    END IF;
END
$$;

CREATE TABLE IF NOT EXISTS coink_app.municipality (
    id             SERIAL PRIMARY KEY,
    department_id  INT NOT NULL,
    name           VARCHAR(100) NOT NULL
);

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.table_constraints tc
        WHERE tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'coink_app'
          AND tc.table_name   = 'municipality'
          AND tc.constraint_name = 'fk_municipality_department'
    ) THEN
        ALTER TABLE coink_app.municipality
        ADD CONSTRAINT fk_municipality_department
        FOREIGN KEY (department_id)
        REFERENCES coink_app.department(id);
    END IF;
END
$$;


CREATE TABLE IF NOT EXISTS coink_app."user" (
    id              SERIAL PRIMARY KEY,
    full_name       VARCHAR(150) NOT NULL,
    phone           VARCHAR(20)  NOT NULL,
    address         VARCHAR(250) NOT NULL,
    country_id      INT NOT NULL,
    department_id   INT NOT NULL,
    municipality_id INT NOT NULL,
    created_at      TIMESTAMP NOT NULL DEFAULT NOW()
);


DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.table_constraints tc
        WHERE tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'coink_app'
          AND tc.table_name   = 'user'
          AND tc.constraint_name = 'fk_user_country'
    ) THEN
        ALTER TABLE coink_app."user"
        ADD CONSTRAINT fk_user_country
        FOREIGN KEY (country_id)
        REFERENCES coink_app.country(id);
    END IF;
END
$$;


DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.table_constraints tc
        WHERE tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'coink_app'
          AND tc.table_name   = 'user'
          AND tc.constraint_name = 'fk_user_department'
    ) THEN
        ALTER TABLE coink_app."user"
        ADD CONSTRAINT fk_user_department
        FOREIGN KEY (department_id)
        REFERENCES coink_app.department(id);
    END IF;
END
$$;

DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.table_constraints tc
        WHERE tc.constraint_type = 'FOREIGN KEY'
          AND tc.table_schema = 'coink_app'
          AND tc.table_name   = 'user'
          AND tc.constraint_name = 'fk_user_municipality'
    ) THEN
        ALTER TABLE coink_app."user"
        ADD CONSTRAINT fk_user_municipality
        FOREIGN KEY (municipality_id)
        REFERENCES coink_app.municipality(id);
    END IF;
END
$$;


CREATE INDEX IF NOT EXISTS idx_country_name
    ON coink_app.country (name);

CREATE INDEX IF NOT EXISTS idx_department_country
    ON coink_app.department (country_id);

CREATE INDEX IF NOT EXISTS idx_municipality_department
    ON coink_app.municipality (department_id);

CREATE INDEX IF NOT EXISTS idx_user_country
    ON coink_app."user" (country_id);

CREATE INDEX IF NOT EXISTS idx_user_department
    ON coink_app."user" (department_id);

CREATE INDEX IF NOT EXISTS idx_user_municipality
    ON coink_app."user" (municipality_id);