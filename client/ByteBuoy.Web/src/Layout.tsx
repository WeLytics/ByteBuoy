import { Outlet } from 'react-router-dom';
import Header from './components/Nav';
import Footer from './components/Footer';

const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <main>
      <div className="mx-auto max-w-7xl sm:px-6 lg:px-8 dark:text-white">

        <Outlet /> {/* Child routes will be rendered here */}

        </div>  
      </main>
      <Footer />
    </>
  );
}

export default Layout;
