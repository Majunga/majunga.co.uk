import Link from 'next/link'
import 'isomorphic-fetch'

export default class Portfolio extends React.Component {
    async componentDidMount(props) {
            this.setState({repos: {}});
            var json = await (await fetch('https://api.github.com/users/majunga/repos')).json()
            this.setState({repos: json})
               
    }
    render() {
        return (
            <div className="portfolio">
                <h2><Link href="https://github.com/Majunga"><a>My Github</a></Link></h2>
                
                {
                    (this.state && this.state.repos && this.state.repos.length > 0) ? 
                    <div>
                        { 
                            this.state.repos.reverse(repo => repo.updated_at).map(repo =>
                                <div key="{repo.id}">
                                    <h3><Link href={repo.html_url}><a>{repo.name}</a></Link></h3>
                                    <p>{repo.description}</p>
                                </div>
                            )
                        }
                    </div>
                    : <p>No Repos to display</p>
                }

                <style jsx>{`
                        a {
                            color : #333;
                        }
                `}</style>
            </div>
        )
    }
}