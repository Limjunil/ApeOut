using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TMPShaking : MonoBehaviour
{
    public float AngleMultiplier = default;
    public float CurveScale = default;

    public TMP_Text tmpTxt;

    public bool hasTextChanged;


    private struct TmpAnim
    {
        public float angleRange;
        public float angle;
        public float speed;
    }

    void OnEnable()
    {
        
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    // Start is called before the first frame update
    void Awake()
    {
        tmpTxt = gameObject.GetComponent<TMP_Text>();

        AngleMultiplier = 1f;
        CurveScale = 2f;
    }

    private void Start()
    {
        StartCoroutine(ShakeTMP());
    }

    
    public void ON_TEXT_CHANGED(Object obj_)
    {
        if (obj_ == tmpTxt)
        {
            hasTextChanged = true;

        }
    }

    IEnumerator ShakeTMP()
    {
        tmpTxt.ForceMeshUpdate();

        TMP_TextInfo textInfo_ = tmpTxt.textInfo;

        Matrix4x4 matrix_;

        int loopCount_ = 0;

        hasTextChanged = true;

        // 미리 여러 문자에 대해 계산된 각도와 범위 및 속도를 포함하는 배열을 만듬
        TmpAnim[] tmpAnim_ = new TmpAnim[50];

        for (int i = 0; i < 50; i++)
        {
            tmpAnim_[i].angleRange = Random.Range(10f, 25f);
            tmpAnim_[i].speed = Random.Range(1f, 3f);
        }

        // 텍스트의 개체 정적 정보를 캐싱한다
        TMP_MeshInfo[] cachedMeshInfo_ = textInfo_.CopyMeshInfoVertexData();

        while (true)
        {
            // 텍스트가 변경되면 새로 가져오는 조건
            if (hasTextChanged == true)
            {
                cachedMeshInfo_ = textInfo_.CopyMeshInfoVertexData();

                hasTextChanged = false;
            }

            int characterCount_ = textInfo_.characterCount;

            // 텍스트가 없다면 넘어간다
            if (characterCount_ == 0)
            {                
                continue;
            }


            for (int i = 0; i < characterCount_; i++)
            {
                TMP_CharacterInfo charInfo_ = textInfo_.characterInfo[i];

                // 표시되지 않는 문자는 건너 뛴다
                if (!charInfo_.isVisible)
                {
                    continue;
                }

                // 현재 i 값에 맞는, 사전 계산된 애니메이션 데이터를 검색함
                TmpAnim txtAnim_ = tmpAnim_[i];

                // 현재 문자에서 사용하는 Index를 가져옴
                int materialIndex_ = textInfo_.characterInfo[i].materialReferenceIndex;

                // 현재 문자에서 사용하는 첫번째 Index를 가져옴
                int vertexIndex_ = textInfo_.characterInfo[i].vertexIndex;

                // 이 문자에서 사용되는 메시의 캐시된 정보를 가져옴
                Vector3[] sourceVertices_ = cachedMeshInfo_[materialIndex_].vertices;

                // 각 문자의 중심점을 결정합니다.
                Vector2 charMidPos_ =
                    (sourceVertices_[vertexIndex_ + 0] + sourceVertices_[vertexIndex_ + 2]) / 2;

                // 각 4개의 정점을 모두 문자/기준선의 중간에 맞게 변환함
                // 행렬 TRS가 각 문자의 원점에 적용되도록 하려면 이 작업이 필요함
                Vector3 offset_ = charMidPos_;

                Vector3[] destinationVertices_ = textInfo_.meshInfo[materialIndex_].vertices;

                destinationVertices_[vertexIndex_ + 0] = sourceVertices_[vertexIndex_ + 0] - offset_;
                destinationVertices_[vertexIndex_ + 1] = sourceVertices_[vertexIndex_ + 1] - offset_;
                destinationVertices_[vertexIndex_ + 2] = sourceVertices_[vertexIndex_ + 2] - offset_;
                destinationVertices_[vertexIndex_ + 3] = sourceVertices_[vertexIndex_ + 3] - offset_;

                txtAnim_.angle =
                    Mathf.SmoothStep(-txtAnim_.angleRange, txtAnim_.angleRange,
                    Mathf.PingPong(loopCount_ / 25f * txtAnim_.speed, 1f));
                
                Vector3 jitterOffset_ = new Vector3(Random.Range(-.25f, .25f), Random.Range(-.25f, .25f), 0);

                matrix_ = Matrix4x4.TRS(jitterOffset_ * CurveScale, Quaternion.Euler(0, 0, Random.Range(-5f, 5f) * AngleMultiplier), Vector3.one);

                destinationVertices_[vertexIndex_ + 0] = matrix_.MultiplyPoint3x4(destinationVertices_[vertexIndex_ + 0]);
                destinationVertices_[vertexIndex_ + 1] = matrix_.MultiplyPoint3x4(destinationVertices_[vertexIndex_ + 1]);
                destinationVertices_[vertexIndex_ + 2] = matrix_.MultiplyPoint3x4(destinationVertices_[vertexIndex_ + 2]);
                destinationVertices_[vertexIndex_ + 3] = matrix_.MultiplyPoint3x4(destinationVertices_[vertexIndex_ + 3]);

                destinationVertices_[vertexIndex_ + 0] += offset_;
                destinationVertices_[vertexIndex_ + 1] += offset_;
                destinationVertices_[vertexIndex_ + 2] += offset_;
                destinationVertices_[vertexIndex_ + 3] += offset_;

                tmpAnim_[i] = txtAnim_;
            }

            // Push changes into meshes
            for (int i = 0; i < textInfo_.meshInfo.Length; i++)
            {
                textInfo_.meshInfo[i].mesh.vertices = textInfo_.meshInfo[i].vertices;
                tmpTxt.UpdateGeometry(textInfo_.meshInfo[i].mesh, i);
            }

            loopCount_ += 1;

            yield return new WaitForSeconds(0.1f);
        }

    }


}
