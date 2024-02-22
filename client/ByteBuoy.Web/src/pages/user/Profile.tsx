import React from 'react';
import ProfileForm from '../../components/ProfileForm';

const ProfilePage: React.FC = () => {
    return (
        <div className='mx-auto max-w-xl px-4 sm:px-6 sm:py-10 lg:px-8 text-left'>
            <ProfileForm />
        </div>
    );
};

export default ProfilePage;