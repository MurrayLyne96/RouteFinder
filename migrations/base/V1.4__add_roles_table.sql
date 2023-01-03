CREATE TABLE roles (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    role_name varchar,
    role_description varchar,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE users
ADD role_id uuid,
DROP COLUMN role,
ADD CONSTRAINT fk_role FOREIGN KEY (role_id)
    REFERENCES roles(id);

INSERT INTO roles(role_name, role_description)
VALUES ('ADM', 'Administrator');

INSERT INTO roles(role_name, role_description)
VALUES('USR', 'User');
