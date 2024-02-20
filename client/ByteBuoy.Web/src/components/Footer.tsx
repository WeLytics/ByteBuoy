import React from "react";

interface NavLink {
	name: string;
	href: string;
}

interface SocialLink {
	name: string;
	href: string;
	icon: (props: React.SVGProps<SVGSVGElement>) => JSX.Element;
}

const navigation: {
	main: NavLink[];
	social: SocialLink[];
} = {
	main: [
		{ name: "Documentation", href: "https://welytics.github.io/ByteBuoy/" },
	],
	social: [
		{
			name: "GitHub",
			href: "https://github.com/WeLytics/ByteBuoy",
			icon: (props: React.SVGProps<SVGSVGElement>) => (
				<svg fill="currentColor" viewBox="0 0 24 24" {...props}>
					<path
						fillRule="evenodd"
						d="M12 2C6.477 2 2 6.484 2 12.017c0 4.425 2.865 8.18 6.839 9.504.5.092.682-.217.682-.483 0-.237-.008-.868-.013-1.703-2.782.605-3.369-1.343-3.369-1.343-.454-1.158-1.11-1.466-1.11-1.466-.908-.62.069-.608.069-.608 1.003.07 1.531 1.032 1.531 1.032.892 1.53 2.341 1.088 2.91.832.092-.647.35-1.088.636-1.338-2.22-.253-4.555-1.113-4.555-4.951 0-1.093.39-1.988 1.029-2.688-.103-.253-.446-1.272.098-2.65 0 0 .84-.27 2.75 1.026A9.564 9.564 0 0112 6.844c.85.004 1.705.115 2.504.337 1.909-1.296 2.747-1.027 2.747-1.027.546 1.379.202 2.398.1 2.651.64.7 1.028 1.595 1.028 2.688 0 3.848-2.339 4.695-4.566 4.943.359.309.678.92.678 1.855 0 1.338-.012 2.419-.012 2.747 0 .268.18.58.688.482A10.019 10.019 0 0022 12.017C22 6.484 17.522 2 12 2z"
						clipRule="evenodd"
					/>
				</svg>
			),
		},
	],
};

const Footer: React.FC = () => {
	return (
    <footer >
			<div className="mx-auto max-w-7xl overflow-hidden px-6 py-6 lg:px-8">
        <div className="mt-16 border-t border-white/10 pt-8 sm:mt-20 md:flex md:items-top md:justify-between lg:mt-24">
          <div className="flex space-x-6 md:order-2">
            {navigation.social.map((item) => (
              <a key={item.name} href={item.href} className="text-gray-500 hover:text-gray-400">
                <span className="sr-only">{item.name}</span>
                <item.icon className="h-6 w-6" aria-hidden="true" />
              </a>
            ))}
          </div>
          {navigation.main.map((item) => (
						<div key={item.name} className="pb-6">
							<a
								href={item.href}
								className="text-sm leading-6 text-white hover:text-white-900"
							>
								{item.name}
							</a>
						</div>
					))}
        </div>
			</div>
		</footer>
	);
};

export default Footer;
