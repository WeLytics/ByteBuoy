import { Outlet } from 'react-router-dom';
import Header from './components/Nav';
import Footer from './components/Footer';

const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <main>
        <Outlet /> {/* Child routes will be rendered here */}
      </main>
      <Footer />
    </>
  );
}

export default Layout;
