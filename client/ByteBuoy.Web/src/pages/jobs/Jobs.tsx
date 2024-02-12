import React from 'react';
import PageTitle from '../../components/PageTitle';
import JobsList from './JobsList';


const Jobs: React.FC = () => {
  return (
    <>
      <PageTitle title="Jobs ⛵" />
      <JobsList />
    </>
  )
}

export default Jobs;
