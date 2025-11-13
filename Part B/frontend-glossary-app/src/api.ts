import type { GlossaryItem } from './types';

const BASE = '/api/Glossary';

async function req<T>(url: string, init?: RequestInit): Promise<T> {
    const res = await fetch(url, { headers: { 'Content-Type': 'application/json' }, ...init });

    if (!res.ok) 
        throw new Error(await res.text().catch(() => res.statusText));

    if (res.status === 204) 
        return undefined as T;
    
    const txt = await res.text();
    return (txt ? JSON.parse(txt) : null) as T;
}

export const api = {
    list: () => req<GlossaryItem[]>(BASE),
    create: (p: Omit<GlossaryItem, 'id'>) => req<GlossaryItem>(BASE, { method: 'POST', body: JSON.stringify(p) }),
    update: (id: string, p: Omit<GlossaryItem, 'id'>) => req<GlossaryItem>(`${BASE}/${id}`, { method: 'PUT', body: JSON.stringify(p) }),
    remove: (id: string) => req<void>(`${BASE}/${id}`, { method: 'DELETE' }),
};