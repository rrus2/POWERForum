import React, { Component, useState } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';



export default function NavMenu() {
    const displayName = NavMenu.name;
    const [isAuthenticated, userHasAuthenticated] = useState(false);

    function handleLogout() {
        isAuthenticated(false);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            {isAuthenticated ? (
                                <NavItem tag={Link} className="text-dark" onClick={handleLogout}>Logout</NavItem>
                                )
                                :
                                (
                                    <div>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/register">Register</NavLink>
                                        </NavItem>
                                        <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                                        </NavItem>
                                    </div>
                                )
                            }
                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}