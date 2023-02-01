import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },

  {
    name: 'Common',
    title: true
  },
  {
    name: 'Organization',
    url: '/organization',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Profile',
        url: '/organization/profile'
      }, {
        name: 'Branches',
        url: '/organization/branches'
      },{
        name:'Structures',
        url:'/organization/structure'
      }
    ]
  },
  {
    name: 'Budget Year',
    url: '/budgetyear',
    iconComponent: { name: 'cil-calendar' }
 
  },

];
