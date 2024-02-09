import React from 'react';

// Define an interface for the icon props
interface IconProps extends React.SVGProps<SVGSVGElement> {}

// Define an interface for each navigation item
interface NavigationItem {
  name: string;
  href: string;
  icon: React.FC<IconProps>; // Using React Functional Component with IconProps
}

// Define the navigation array with typed objects
const navigation: NavigationItem[] = [
  {
    name: 'Facebook',
    href: '#',
    icon: (props: IconProps) => (
      <svg fill="currentColor" viewBox="0 0 24 24" {...props}>
        {/* SVG Path */}
      </svg>
    ),
  },
  {
    name: 'Instagram',
    href: '#',
    icon: (props: IconProps) => (
      <svg fill="currentColor" viewBox="0 0 24 24" {...props}>
        {/* SVG Path */}
      </svg>
    ),
  },
  // Add other items similarly
];

// Example component with TypeScript
const Footer: React.FC = () => {
  return (
    <footer className="bg-white mt-5">
      <div className="mx-auto max-w-7xl px-6 py-12 md:flex md:items-center md:justify-between lg:px-8">
        <div className="flex justify-center space-x-6 md:order-2">
          {navigation.map((item) => (
            <a key={item.name} href={item.href} className="text-gray-400 hover:text-gray-500">
              <span className="sr-only">{item.name}</span>
              <item.icon className="h-6 w-6" aria-hidden="true" />
            </a>
          ))}
        </div>
        <div className="mt-8 md:order-1 md:mt-0">
          <p className="text-center text-xs leading-5 text-gray-500">
            {/* &copy; 2020 Your Company, Inc. All rights reserved. */}
          </p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
