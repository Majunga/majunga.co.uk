import {Jumbotron} from 'react-bootstrap'
import Link from 'next/link'
const MyInfo = () =>

<div>
    <Jumbotron >
        <h1>Hello, <p>My name is Ethan McWilliams and welcome to my website.</p></h1>
        <p>This website was made to display things about myself and what I've been creating. 
            If you would like to find out more about me, please head to my <Link href="https://www.linkedin.com/in/ethan-mcwilliams-92a02a3b/"><a>LinkedIn</a></Link></p>
    </Jumbotron>
</div>

export default MyInfo