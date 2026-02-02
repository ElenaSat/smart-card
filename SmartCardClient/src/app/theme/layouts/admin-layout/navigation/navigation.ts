export interface NavigationItem {
  id: string;
  title: string;
  type: 'item' | 'collapse' | 'group';
  translate?: string;
  icon?: string;
  hidden?: boolean;
  url?: string;
  classes?: string;
  groupClasses?: string;
  exactMatch?: boolean;
  external?: boolean;
  target?: boolean;
  breadcrumbs?: boolean;
  children?: NavigationItem[];
  link?: string;
  description?: string;
  path?: string;
}

export const NavigationItems: NavigationItem[] = [
  {
    id: 'features',
    title: 'Gestión',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      {
        id: 'usuarios',
        title: 'Usuarios',
        type: 'item',
        url: '/usuarios',
        icon: 'user',
        breadcrumbs: true
      },
      {
        id: 'cuentas',
        title: 'Cuentas',
        type: 'item',
        url: '/cuentas',
        icon: 'bank',
        breadcrumbs: true
      },
      {
        id: 'tarjetas',
        title: 'Tarjetas',
        type: 'item',
        url: '/tarjetas',
        icon: 'credit-card',
        breadcrumbs: true
      },
      {
        id: 'tipo-tarjetas',
        title: 'Tipos de Tarjeta',
        type: 'item',
        url: '/tipo-tarjetas',
        icon: 'tags',
        breadcrumbs: true
      },
      {
        id: 'formato-tarjetas',
        title: 'Formatos de Tarjeta',
        type: 'item',
        url: '/formato-tarjetas',
        icon: 'barcode',
        breadcrumbs: true
      },
      {
        id: 'paises',
        title: 'Países',
        type: 'item',
        url: '/paises',
        icon: 'global',
        breadcrumbs: true
      },
      {
        id: 'usuario-sistemas',
        title: 'Usuarios Ssitema',
        type: 'item',
        url: '/usuario-sistemas',
        icon: 'team',
        breadcrumbs: true
      }
    ]
  }
];
