type Props = {
    editing: boolean;
    term: string;
    definition: string;
    onTermChange: (v: string) => void;
    onDefinitionChange: (v: string) => void;
    onSubmit: () => void
    onCancel: () => void;
};

export default function GlossaryForm(props: Props) {
    const {
        editing, term, definition,
        onTermChange, onDefinitionChange,
        onSubmit, onCancel
    } = props;

    function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        onSubmit();
    }

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', gap: 8, marginBottom: 16 }}>
            <input
                placeholder="Term"
                value={term}
                onChange={(e) => onTermChange(e.target.value)}
                required
                style={{ flex: 1 , width: "100px", height: "40px"}}
            />
            <input
                placeholder="Definition"
                value={definition}
                onChange={(e) => onDefinitionChange(e.target.value)}
                required
                style={{ flex: 3 }}
            />
            <button type="submit">{editing ? 'Update' : 'Add'}</button>
            {editing && (
                <button type="button" onClick={onCancel}>Cancel</button>
            )}
        </form>
    );
}
