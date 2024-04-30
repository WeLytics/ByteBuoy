import React, { useState, useEffect } from 'react';

interface CircularProgressProps {
  size: number;
  strokeWidth: number;
  duration: number; // in seconds
}

const CircularProgress: React.FC<CircularProgressProps> = ({ size, strokeWidth, duration }) => {
  const [progress, setProgress] = useState(0);
  const radius = (size - strokeWidth) / 2;
  const circumference = radius * 2 * Math.PI;

  useEffect(() => {
    const interval = setInterval(() => {
      setProgress((oldProgress) => {
        if (oldProgress < 100) return oldProgress + 100 / duration;
        clearInterval(interval);
        return 100;
      });
    }, 1000);

    return () => clearInterval(interval);
  }, [duration]);

  const circleStyle = {
    transition: 'stroke-dashoffset 1s linear',
    transform: `rotate(-90 ${size / 2} ${size / 2})`
  };

return (
    <span style={{ display: 'inline-block' }}>
        <svg width={size} height={size} style={{ display: 'inline' }}>
            <circle
                stroke="lightgray"
                fill="transparent"
                strokeWidth={strokeWidth}
                r={radius}
                cx={size / 2}
                cy={size / 2}
            />
            <circle
                stroke="gray"
                fill="transparent"
                strokeWidth={strokeWidth}
                strokeDasharray={circumference + ' ' + circumference}
                style={{ strokeDashoffset: circumference - (progress / 100) * circumference, ...circleStyle }}
                r={radius}
                cx={size / 2}
                cy={size / 2}
            />
        </svg>
    </span>
);
};

export default CircularProgress;
