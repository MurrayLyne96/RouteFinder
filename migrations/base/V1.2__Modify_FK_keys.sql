ALTER TABLE plotpoints
DROP CONSTRAINT fk_route,
ADD CONSTRAINT fk_route FOREIGN KEY (route_id)
        REFERENCES routes(id) ON DELETE CASCADE;