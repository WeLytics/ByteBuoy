import React from 'react';
import PageTitle from '../../components/PageTitle';
import PagesList from './PagesList';


const PagesComponent: React.FC = () => {
  
  return (
    <>
      <PageTitle title="Pages" />
      <PagesList />
    </>
  )
}

export default PagesComponent;
