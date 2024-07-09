import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';

const config: Config = {
  title: 'ByteBuoy Docs',
  tagline: 'OpenSource Artefact Monitoring',
  favicon: 'img/favicon.ico',

  // Set the production url of your site here
  url: 'https://bytebuoy.app',
  // Set the /<baseUrl>/ pathname under which your site is served
  // For GitHub pages deployment, it is often '/<projectName>/'
  baseUrl: '/ByteBuoy/',

  // GitHub pages deployment config.
  // If you aren't using GitHub pages, you don't need these.
  organizationName: 'welytics', // Usually your GitHub org/user name.
  projectName: 'bytebuoy', // Usually your repo name.

  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',

  // Even if you don't use internationalization, you can use this field to set
  // useful metadata like html lang. For example, if your site is Chinese, you
  // may want to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          routeBasePath: '/', 
          sidebarPath: './sidebars.ts',
          editUrl:
            'https://github.com/WeLytics/ByteBuoy/tree/main/docs/docs/',
        },
        blog: false, // Optional: disable the blog plugin
       
	   theme: {
          customCss: './src/css/custom.css',
        },
      } satisfies Preset.Options,
    ],
  ],

  themeConfig: {
    // Replace with your project's social card
    image: 'img/docusaurus-social-card.jpg',
    navbar: {
      title: 'ByteBuoy Docs',
      logo: {
        alt: 'ByteBuoy Docs',
        src: 'img/logo_bytebuoy.png',
      },
      items: [
          
	   {
          type: 'docSidebar',
          sidebarId: 'tutorialSidebar',
          position: 'left',
          label: 'Docs',
        },
		// uncomment to display version
		// {
          // type: 'docsVersionDropdown',
        // },
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            {
              label: 'Docs',
              to: '/',
              // to: '/docs/getting-started',
            },
          ],
        },
        {
          title: 'Community',
          items: [
            {
              label: 'GitHub',
              href: 'https://github.com/welytics/ByteBuoy',
            },
            {
              label: 'Stack Overflow',
              href: 'https://stackoverflow.com/questions/tagged/bytebuoy',
            },
            {
              label: 'Discord',
              href: 'https://discord.gg/9ujA3fme',
            },
          ],
        }
      ],
      copyright: `Copyright © ${new Date().getFullYear()} ByteBuoy, Inc. Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
