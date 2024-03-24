import React from 'react';
import PageTitle from '../../components/PageTitle';
import PagesList from './PagesList';


const PagesComponent: React.FC = () => {
  
  return (
    <>
      <PageTitle title="Metric Pages" />
      <PagesList />
    </>
  )
}

export default PagesComponent;
