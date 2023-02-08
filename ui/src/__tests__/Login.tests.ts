import { render, screen } from '@testing-library/react';
import Login from '../pages/login/index';

test('renders the Login Page', () => {
    render(Login());
});