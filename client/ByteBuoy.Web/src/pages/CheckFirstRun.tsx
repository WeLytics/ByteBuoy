// import { useEffect, useState } from 'react';
// import { useNavigate } from 'react-router-dom';
// import { fetchData } from "../../services/apiService";


// const CheckFirstRun = () => {
//   const [isFirstRun, setIsFirstRun] = useState<boolean | null>(null);
//   const navigate = useNavigate();

//   useEffect(() => {
//     const checkFirstRun = async () => {
//       try {
//         const response = await fetch('/api/v1/system/isFirstRun');
//         const data = await response.json();
//         setIsFirstRun(data.isFirstRun);
//       } catch (error) {
//         console.error('Failed to check first run status:', error);
//         setIsFirstRun(false); // Assume it's not the first run if there's an error
//       }
//     };

//     checkFirstRun();
//   }, []);

//   useEffect(() => {
//     if (isFirstRun === null) {
//       // Still checking, do nothing yet
//       return;
//     }

//     if (isFirstRun) {
//       navigate('/setup');
//     } else {
//       navigate('/'); // or use navigate(-1) to go back to the previous route if that's desired behavior
//     }
//   }, [isFirstRun, navigate]);

//   // Optionally, show a loading indicator or return null while checking
//   return isFirstRun === null ? <div>Loading...</div> : null;
// };

// export default CheckFirstRun;
