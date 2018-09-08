import MyInfo from '../components/MyInfo.js'
import { Grid } from 'react-bootstrap'
import Portfolio from '../components/Portfolio.js';
import Layout from '../components/Layout.js';

const Index = () =>
<Layout>
    <div>
        <Grid>
            <MyInfo/>
            <Portfolio/>
        </Grid>
    </div>
</Layout>
export default Index