CREATE extension IF NOT EXISTS "uuid-ossp";

CREATE TABLE users (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    email_address varchar,
    first_name varchar,
    last_name varchar,
    password varchar,
    role varchar,
    date_of_birth timestamp,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE types (
    id serial PRIMARY KEY,
    name varchar,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tags (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    name varchar,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE routes (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    route_name varchar,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP,
    type_id integer,
    user_id uuid,
    CONSTRAINT fk_type FOREIGN KEY (type_id)
        REFERENCES types(id),
    CONSTRAINT fk_user FOREIGN KEY (user_id)
        REFERENCES users(id)
);

CREATE TABLE plotpoints (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    x_coordinate float,
    y_coordinate float,
    point_description varchar,
    plot_order integer,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP,
    route_id uuid,
    CONSTRAINT fk_route FOREIGN KEY (route_id)
        REFERENCES routes(id)
);

CREATE TABLE reviews (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    content varchar,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP,
    route_id uuid,
    user_id uuid,
    CONSTRAINT fk_route FOREIGN KEY (route_id)
        REFERENCES routes(id),
    CONSTRAINT fk_user FOREIGN KEY (user_id)
        REFERENCES users(id)
);

CREATE TABLE ratings (
    id uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    rating float,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP,
    route_id uuid,
    user_id uuid,
    CONSTRAINT fk_route FOREIGN KEY (route_id)
        REFERENCES routes(id),
    CONSTRAINT fk_user FOREIGN KEY (user_id)
        REFERENCES users(id)
);

CREATE TABLE route_tags (
    id serial PRIMARY KEY,
    created timestamp DEFAULT CURRENT_TIMESTAMP,
    last_modified timestamp DEFAULT CURRENT_TIMESTAMP,
    route_id uuid,
    tag_id uuid,
    CONSTRAINT fk_route FOREIGN KEY (route_id)
        REFERENCES DONOTWORK(id),
    CONSTRAINT fk_tag FOREIGN KEY (tag_id)
        REFERENCES tags(id)
);
