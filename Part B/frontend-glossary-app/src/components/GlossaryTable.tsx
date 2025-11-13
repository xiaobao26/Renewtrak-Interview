type Item = { id: string; term: string; definition: string };

type Props = {
    items: Item[];
    onEdit: (item: Item) => void;
    onDelete: (id: string) => void;
};

export default function GlossaryTable({ items, onEdit, onDelete }: Props) {
    if (items.length === 0) return <p>No terms.</p>;

    return (
        <table border={1} cellPadding={8} cellSpacing={0} width="100%">
            <thead style={{ background: '#eee' }}>
                <tr>
                    <th style={{ textAlign: 'left', width: '25%' }}>Term</th>
                    <th style={{ textAlign: 'left' }}>Definition</th>
                    <th style={{ textAlign: 'left', width: 160 }}>Actions</th>
                </tr>
            </thead>
            <tbody>
                {items.map(x => (
                    <tr key={x.id}>
                        <td>{x.term}</td>
                        <td>{x.definition}</td>
                        <td>
                            <button onClick={() => onEdit(x)} style={{ marginRight: 8 }}>Edit</button>
                            <button onClick={() => onDelete(x.id)}>Delete</button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}