import React from 'react';

type Props = {
    title: string;
};

const PageTitle: React.FC<Props> = ({ title }) => {
    return <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
              <h1 className="text-3xl font-bold leading-tight tracking-tight text-gray-900 dark:text-white">{title}</h1>
            </div>
};

export default PageTitle;