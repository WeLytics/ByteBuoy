import React, { useState } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { CreatePageAsync } from '../services/apiService';
import '../App.css'

const SetupComponent = () => {
  const [pageName, setPageName] = useState<string>('');
  const [loading, setLoading] = useState<boolean>(false);

  const validateForm = (): boolean => {
    if (pageName.trim().length === 0 || pageName.length > 50) {
      toast.error('Page name is required and must be less than 50 characters.');
      return false;
    }
    return true;
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>): Promise<void> => {
    event.preventDefault();
    if (!validateForm()) return;

    setLoading(true);
    try {
      const newPageId = await CreatePageAsync(pageName);
      toast.success('Page created successfully!');
      setTimeout(() => {
        window.location.href = `/pages/${newPageId}`;
      }, 3000); // Redirect after showing success message
    } catch (error) {
      toast.error('Failed to create page.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <h2>Setup Page</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="pageName">Page Name</label>
          <input
            type="text"
            id="pageName"
            value={pageName}
            onChange={(e) => setPageName(e.target.value)}
            required
            maxLength={50}
          />
        </div>
        <button type="submit" disabled={loading}>
          Create Page
        </button>
      </form>
      <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} newestOnTop={false} closeOnClick rtl={false} pauseOnFocusLoss draggable pauseOnHover />
    </div>
  );
};

export default SetupComponent;
