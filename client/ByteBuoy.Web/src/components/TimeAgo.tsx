import React from 'react';
import moment from 'moment';

interface TimeAgoProps {
  dateString: string;
}

const TimeAgo: React.FC<TimeAgoProps> = ({ dateString }) => {
    if (!dateString) return <></>; 

    // Convert the UTC dateString to a moment object in the local timezone
    const date = moment.utc(dateString).local();
    
    // Calculate the time ago in words, automatically converted to the local timezone
    const timeAgo = date.fromNow();
    
    // Format the date in a readable format, considering the local timezone
    const formattedDate = date.format('MM/DD/YYYY, HH:mm:ss'); // Example format

    return (
        <span title={formattedDate}>{timeAgo}</span>
    );
};

export default TimeAgo;
