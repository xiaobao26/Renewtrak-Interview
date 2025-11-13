import { useEffect, useState } from 'react';
import GlossaryForm from './components/GlossaryForm';
import GlossaryTable from './components/GlossaryTable';

const API_BASE = (import.meta.env.VITE_API_BASE ?? '').replace(/\/+$/, '');
const BASE = `${API_BASE}/api/glossary-terms`;

type Item = { id: string; term: string; definition: string };


function toMsg(e: unknown): string {
  if (e instanceof Error) return e.message;
  if (typeof e === 'string') return e;
  try { return JSON.stringify(e); } catch { return 'Unknown error'; }
}

async function http<T>(url: string, init?: RequestInit): Promise<T> {
  const res = await fetch(url, { headers: { 'Content-Type': 'application/json' }, ...init });
  if (!res.ok) throw new Error(await res.text().catch(() => res.statusText));
  const txt = await res.text();
  return (txt ? JSON.parse(txt) : null) as T;
}

export default function App() {
  const [items, setItems] = useState<Item[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  const [editingItem, setEditingItem] = useState<Item | null>(null);
  const [term, setTerm] = useState('');
  const [definition, setDefinition] = useState('');

  async function load() {
    try {
      setLoading(true);
      setError('');
      const data = await http<Item[]>(BASE);
      const sorted = [...(data || [])].sort((a, b) => a.term.localeCompare(b.term));
      setItems(sorted);
    } catch (e: unknown) {
      setError(toMsg(e));
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => { load(); }, []);

  function startEdit(x: Item) {
    setEditingItem(x);
    setTerm(x.term);
    setDefinition(x.definition);
  }

  function resetForm() {
    setEditingItem(null);
    setTerm('');
    setDefinition('');
  }

  async function submit() {
    try {
      const t = term.trim();
      const d = definition.trim();

      if (editingItem) {
        await http<Item>(`${BASE}/${editingItem.id}`, {
          method: 'PUT',
          body: JSON.stringify({ term: t, definition: d }),
        });
      } else {
        await http<Item>(BASE, {
          method: 'POST',
          body: JSON.stringify({ term: t, definition: d}),
        });
      }

      resetForm();
      await load();
    } catch (e: unknown) {
      setError(toMsg(e));
    }
  }

  async function remove(id: string) {
    if (!confirm('Delete this term?')) return;
    try {
      await http<void>(`${BASE}/${id}`, { method: 'DELETE' });
      await load();
    } catch (e: unknown) {
      setError(toMsg(e));
    }
  }

  return (
    <div style={{ display: 'grid', placeItems: 'center', minHeight: '100vh' }}>
      <div style={{ width: '100%', maxWidth: 900, padding: 16, fontFamily: 'sans-serif' }}>
        <h2>Glossary</h2>

        <GlossaryForm
          editing={!!editingItem}
          term={term}
          definition={definition}
          onTermChange={setTerm}
          onDefinitionChange={setDefinition}
          onSubmit={submit}
          onCancel={resetForm}
        />

        <div style={{ marginBottom: 8 }}>
          <button onClick={load}>Reload</button>
          {error && <span style={{ color: 'red', marginLeft: 12 }}>{error}</span>}
        </div>

        {loading ? (
          <p>Loading...</p>
        ) : (
          <GlossaryTable items={items} onEdit={startEdit} onDelete={remove} />
        )}
      </div>
    </div>
  );
}