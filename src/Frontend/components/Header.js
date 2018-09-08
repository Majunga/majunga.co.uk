import { Navbar, Nav, NavItem } from 'react-bootstrap'

const Header = () =>
    <Navbar inverse>
        <Navbar.Header>
            <Navbar.Brand>
                <a href="index">Majunga.co.uk</a>
            </Navbar.Brand>
        </Navbar.Header>
        <Nav>
            <NavItem eventKey={1} href="#portfolio">
                Portfolio
            </NavItem>
        </Nav>
    </Navbar>


export default Header 