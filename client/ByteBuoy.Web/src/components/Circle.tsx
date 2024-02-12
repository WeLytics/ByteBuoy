import React from 'react';
import { classNames } from '../utils/utils';

interface CircleProps {
  colorClass: string
}

const Circle: React.FC<CircleProps> = ({ colorClass }) => {

  return <>

        <div
        className={classNames(
            "inline-block",
            colorClass,
            "flex-none rounded-full p-1", 
            "w-4 h-4"
        )}
        >
        <div className="h-2 w-2 rounded-full bg-current" />
        </div>
    </>;
};

export default Circle;
