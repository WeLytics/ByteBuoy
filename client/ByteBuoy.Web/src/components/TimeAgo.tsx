import React from 'react';
import { parseISO, formatDistanceToNow, format } from 'date-fns';

interface TimeAgoProps {
  dateString: string;
}

const TimeAgo: React.FC<TimeAgoProps> = ({ dateString }) => {
    if (!dateString) return <></>; 

    const date = parseISO(dateString);
    const timeAgo = formatDistanceToNow(date, { addSuffix: true });
    const formattedDate = format(date, 'PPpp'); // Example format: 'MM/dd/yyyy, HH:mm:ss'

    return (
       <>
        DATE {formattedDate}
        <span title={formattedDate}>{timeAgo}</span>
      </>
    );
};

export default TimeAgo;
