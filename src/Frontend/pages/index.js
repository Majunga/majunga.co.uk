import MyInfo from '../components/MyInfo.js'
import Layout from '../components/Layout.js'
import { Grid } from 'react-bootstrap'

const Index = () =>

<div className="App">
    <Layout/>
    <Grid>
        <MyInfo/>
    </Grid>
</div>

export default Index